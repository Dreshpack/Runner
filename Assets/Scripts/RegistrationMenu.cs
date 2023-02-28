using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using UnityEngine.Events;

public class RegistrationMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_InputField _passwordRepeatInput;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _testText;
    //[SerializeField] private GameObject windowController;

    public UnityEvent succesRegister;
    //public DependencyStatus dependencyStatus;
    private FirebaseAuth auth;
    private FirebaseUser _user;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void RegisterCoroutine()
    {
        StartCoroutine(Register(_emailInput.text, _passwordInput.text, _usernameInput.text));
    }

    public void LoginCoroutine()
    {
        StartCoroutine(Login(_emailInput.text, _passwordInput.text));
        
    }
    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_emailInput.text, _passwordInput.text);
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
            Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
            _errorText.text = "";
        }

        /*var LoginTask = auth.SignInWithEmailAndPasswordAsync(_emailInput.text, _passwordInput.text).ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to login task with {task.Exception}");
                FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                Debug.Log("log failed");
                _testText.text = "log failed";
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
                _user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
                _errorText.text = "";
            }
        });*/


    }


    private IEnumerator Register(string _email, string _password, string _username)
    {
        Debug.Log("reg");
        if(_username == "")
        {
            Debug.Log("miss username");
            _errorText.text = "Miss Username";
        }
        else if (_passwordInput.text != _passwordRepeatInput.text)
        {
            _errorText.text = "Password don't match";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            if(RegisterTask.Exception != null)
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
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    }
                    else
                    {
                        _errorText.text = "";
                        succesRegister.Invoke();
                        //StartCoroutine(Login(_email, _password));
                        //Username is now set
                        //Now return to login screen
                    }
                }
            }
        }


    }
}
