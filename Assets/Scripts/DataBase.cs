using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DataBase : MonoBehaviour
{
    DatabaseReference _dataBaseRef;
    [SerializeField] GameObject _leaderBoard;
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
        _dataBaseRef.Child("Users").Child(name).Child("Score").SetValueAsync(score);
    }

    public IEnumerator GetData(string name)
    {
        var userScore = _dataBaseRef.Child("Users").Child(name).GetValueAsync();
        yield return new WaitUntil(predicate: () => userScore.IsCompleted);

        if(userScore.Result == null)
        {
            Debug.Log("Log");
        }
        else
        {
            DataSnapshot snapshot = userScore.Result;
            _leader.text = snapshot.Child("Name").Value.ToString();
            _leader.text += snapshot.Child("Score").Value.ToString();
        }
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
        }
    }
}
