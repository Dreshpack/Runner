using UnityEngine.SceneManagement;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject _regPanel;
    [SerializeField] private GameObject _logPanel;
    [SerializeField] private Scene _regScene;
    [SerializeField] private Scene _gameScene;

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

    public void ChangeScene()
    {
        SceneManager.LoadScene("RunnerScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
