using UnityEngine.SceneManagement;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject _regPanel;
    [SerializeField] private GameObject _logPanel;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            _regPanel.SetActive(true);
            _logPanel.SetActive(false);
        }
    }

    public void ChangeWindowToLogin()
    {
        _logPanel.SetActive(true);
        _regPanel.SetActive(false);
    }

    public void ChangeWindowToReg()
    {
        _logPanel.SetActive(false);
        _regPanel.SetActive(true);
    }

   
   
    public void ChangeSceneToLogin()
    {
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("RunnerScene");
    }

    public void ChangeSceneToRunner()
    {
        SceneManager.LoadScene("RunnerScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
