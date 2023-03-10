using TMPro;
using UnityEngine;
using Firebase.Database;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;

public class LeaderBoardScript : MonoBehaviour
{
    [SerializeField] GameObject _leaderBoard;
    [SerializeField] LeaderUI _leaderUI;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] DataBase _dataBase;

    private List<LeaderUI> _leaders = new List<LeaderUI>();
    private LeaderUI[] _leaderList = new LeaderUI[10];

    public void CloseLeaderboard()
    {
        foreach(LeaderUI leader in _leaders)
        {
            Destroy(leader.gameObject);
        }
        _leaders.Clear();
        _pausePanel.SetActive(true);
        _leaderBoard.SetActive(false);
    }

    public void GetContent()
    {
        _pausePanel.SetActive(false);
        _leaderBoard.SetActive(true);
        StartCoroutine(_dataBase.Sort());
    }    

    public void CreateLeaders(DataSnapshot snapshot)
    {
        int i = 0;
        foreach (DataSnapshot childSnap in snapshot.Children.Reverse<DataSnapshot>().Take(10))
        {
            string name = childSnap.Key.ToString();
            int score = int.Parse(childSnap.Child("Score").Value.ToString());
            var row = Instantiate(_leaderUI, transform).GetComponent<LeaderUI>();
            _leaders.Add(row);
            row.Name.text = name;
            row.Score.text = score.ToString();
        }

    }
}
