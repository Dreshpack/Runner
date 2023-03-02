using UnityEngine.SceneManagement;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Collision _playerCollision;


    private void Lose()
    {
        _losePanel.SetActive(true);
    }

    public void ReloadLvl()
    {
        SceneManager.LoadScene("RunnerSCene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        _losePanel.SetActive(false);
    }

    private void OnEnable()
    {
        _playerCollision.isDead += Lose;
    }
    private void OnDisable()
    {
        _playerCollision.isDead -= Lose;
    }
}