using System;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _score;

    public Action isStarted;

    public void StartRun()
    {
        isStarted?.Invoke();
        Time.timeScale = 1;
        _pauseButton.SetActive(true);
        _score.SetActive(true);
        _startPanel.SetActive(false);
    }
}
