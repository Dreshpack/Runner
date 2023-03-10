using UnityEngine;
using Firebase.Database;
using System;
using System.Collections;
using System.Linq;
using TMPro;

public class DataBase : MonoBehaviour
{
    DatabaseReference _dataBaseRef;
    [SerializeField] LeaderBoardScript _leaderBoard;
    private TMP_Text _leader;

    private void Start()
    {
       _dataBaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SetNameDataBase(string name)
    {
        _dataBaseRef.Child("Users").Child(name).Child("Name").SetValueAsync(name);
        _dataBaseRef.Child("Users").Child(name).Child("Score").SetValueAsync(0);
    }

    public void SetScore(string name, int score)
    {
        StartCoroutine(SetHightScore(name, score));
    }
    public IEnumerator SetHightScore(string name, int score)
    {
        var hightScore = _dataBaseRef.Child("Users").Child(name).Child("Score").GetValueAsync();
        yield return new WaitUntil(predicate: () => hightScore.IsCompleted);

        if(hightScore.Result != null)
        {
            if(Int32.Parse(hightScore.Result.Value.ToString()) < score)
            _dataBaseRef.Child("Users").Child(name).Child("Score").SetValueAsync(score);
        }
        else
        {
            Debug.Log("null");
        }
    }

    public IEnumerator GetData(string name)
    {
        var userScore = _dataBaseRef.Child("Users").Child(name).GetValueAsync();
        yield return new WaitUntil(predicate: () => userScore.IsCompleted);

        if(userScore.Result == null)
        {
            Debug.Log("data null");
        }
        else
        {
            DataSnapshot snapshot = userScore.Result;
            _leader.text = snapshot.Child("Name").Value.ToString();
            _leader.text += snapshot.Child("Score").Value.ToString();
        }
    }

    public IEnumerator FindExistUsername(string name, IEnumerator callback, TMP_Text errorText)
    {
        var users = _dataBaseRef.Child("Users").GetValueAsync();
        yield return new WaitUntil(predicate: () => users.IsCompleted);
        if (users.Result != null)
        {
            bool usernameTaken = false;
            DataSnapshot nameSnapshot = users.Result;
            foreach (DataSnapshot childSnap in nameSnapshot.Children)
            {
                if (name.Equals(childSnap.Key.ToString()))
                {
                    usernameTaken = true;
                    errorText.text = "username is taken";
                    Debug.Log("username is already taken");
                }
            }
            if (!usernameTaken)
            StartCoroutine(callback);
        }
        //callback?.Invoke();
    }

    public IEnumerator Sort()
    {
        var users = _dataBaseRef.Child("Users").OrderByChild("Score").GetValueAsync();
        yield return new WaitUntil(predicate: () => users.IsCompleted);

        if (users.Result == null)
        {
            Debug.Log("Log");
        }
        else
        {
            DataSnapshot snapshot = users.Result;
            _leaderBoard.CreateLeaders(snapshot);
        }
    }
}
