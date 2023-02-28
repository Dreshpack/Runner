using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject _regPanel;
    [SerializeField] private GameObject _logPanel;
    private void Awake()
    {
        _regPanel.SetActive(true);
        _logPanel.SetActive(false);
    }

    public void ChangeWindowToLogin()
    {
        _logPanel.SetActive(true);
        _regPanel.SetActive(false);
    }

    public void ChangeWindowToReg()
    {
        _regPanel.SetActive(true);
        _logPanel.SetActive(false);
    }
    private void HideLogWindow()
    {
        _logPanel.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
