using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using UnityEngine.Events;
using System;

public class RegistrationMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_InputField _passwordRepeatInput;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private WindowController _windowController;

    public UnityEvent _succesRegister;
    private FirebaseAuth auth;
    private FirebaseUser _user;

    private void OnEnable()
    {
        _succesRegister.AddListener(LoginCoroutine);
    }

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        CheckAccount();
    }

    public void RegisterCoroutine()
    {
        //StartCoroutine(Register(_emailInput.text, _passwordInput.text, _usernameInput.text));
        Register(_emailInput.text, _passwordInput.text, _usernameInput.text);
    }

    public void LoginCoroutine()
    {
        StartCoroutine(Login(_emailInput.text, _passwordInput.text));
    }

    private void CheckAccount()
    {
        if(PlayerPrefs.HasKey("Name") && PlayerPrefs.GetString("Name") != "")
        {
            StartCoroutine(Login(PlayerPrefs.GetString("Email"), PlayerPrefs.GetString("Password")));
        }
    }  
    

    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);
        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to login task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string errorMessage = "login failed";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMessage = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    errorMessage = "Missing Password";
                    break;
                case AuthError.UserNotFound:
                    errorMessage = "User not found";
                    break;
            }
            _errorText.text = errorMessage;
        }
        else
        {
            _user = LoginTask.Result;
            PlayerPrefs.SetString("Name", _user.DisplayName);
            PlayerPrefs.SetString("Password", _password);
            PlayerPrefs.SetString("Email", _email);
            Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
            _errorText.text = "";
            _windowController.ChangeSceneToRunner();
        }
    }

    private IEnumerator ContinueRegistration(string _email, string _password, string _username)
    {
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
        if (RegisterTask.Exception != null)
        {
            Debug.Log("error");
            Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            string errorMessage = "Register Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMessage = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    errorMessage = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    errorMessage = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    errorMessage = "Email Already In Use";
                    break;
                case AuthError.InvalidEmail:
                    errorMessage = "Invalid Email";
                    break;
            }
            _errorText.text = errorMessage;
        }
        else
        {
            _user = RegisterTask.Result;
            if (_user != null)
            {
                UserProfile profile = new UserProfile { DisplayName = _username };
                var ProfileTask = _user.UpdateUserProfileAsync(profile);
                Debug.Log("trying to save");
                _dataBase.SetNameDataBase(_username);
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                    FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                }
                else
                {
                    _errorText.text = "";
                    _succesRegister.Invoke();
                }
            }
        }
    }

    private void Register(string _email, string _password, string _username)
    {
        if(_username == "")
        {
            _errorText.text = "Miss Username";
        }
        else if (_passwordInput.text != _passwordRepeatInput.text)
        {
            _errorText.text = "Password don't match";
        }
        StartCoroutine(_dataBase.FindExistUsername(_username, ContinueRegistration(_email, _password, _username), _errorText));
    }
    
}
