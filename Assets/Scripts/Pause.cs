using TMPro;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private TMP_Text _username;
    
    private const string NAME_KEY = "Name";

    private void Start()
    {
        _username.text = PlayerPrefs.GetString(NAME_KEY);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _pauseButton.SetActive(true);
        _pausePanel.SetActive(false);
    }
}
