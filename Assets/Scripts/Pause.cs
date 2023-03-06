using TMPro;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private TMP_Text _Username;
    
    public Action _pauseGame;
    public Action _continueGame;

    private void Start()
    {
        _Username.text = PlayerPrefs.GetString("Name");
    }

    public void PauseGame()
    {
        _pauseGame?.Invoke();
        _pausePanel.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void ContinueGame()
    {
        _continueGame?.Invoke();
        _pauseButton.SetActive(true);
        _pausePanel.SetActive(false);
    }
}
