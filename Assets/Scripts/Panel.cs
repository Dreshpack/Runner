using UnityEngine.SceneManagement;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Collision _playerCollision;
    [SerializeField] private RevivalAds _ads;
    [SerializeField] private GameObject _adsButton;
    [SerializeField] private WindowController _windowController;
    [SerializeField] private GameOverAnim _gameOverAnim;

    private int _amountShownAds = 0;

    public void Lose()
    {
        if (_amountShownAds < 1)
        {
            _adsButton.SetActive(true);
            _amountShownAds++;
        }
        _losePanel.SetActive(true);
        _gameOverAnim.LabelMove();
    }

    public void ReloadLvl()
    {
        SceneManager.LoadScene("RunnerSCene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        _windowController.ChangeSceneToLogin();
    }

    private void Start()
    {
        DisablePanel();
    }

    private void DisablePanel()
    {
        _gameOverAnim.LabelStartPos();
        _adsButton.SetActive(false);
        _losePanel.SetActive(false);
    }

    private void OnEnable()
    {
        _ads.isPlaying += DisablePanel;
    }
    private void OnDisable()
    {
        _ads.isPlaying -= DisablePanel;
    }
}