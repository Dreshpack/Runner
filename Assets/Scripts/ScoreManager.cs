using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private RevivalAds _revivalAds;

    private float _score;
    private float _pointsPerSecond = 1;
    private bool _isDead = false;
    private bool _paused = false;

    private void OnEnable()
    {
        _playerCollision.isDead += OnDie;
        _revivalAds.played += Revive;
    }

    private void OnDisable()
    {
        _playerCollision.isDead -= OnDie;
        _revivalAds.played -= Revive;
    }

    private void OnDie()
    {
        _dataBase.SetScore(PlayerPrefs.GetString("Name"), (int)_score);
        _isDead = true;
    }

    private void Revive()
    {
        _isDead = false;
    }

    private void FixedUpdate()
    {
        if (!_isDead && !_paused)
            UpdateScore();
    }

    private void UpdateScore()
    {
        _score += _pointsPerSecond * Time.deltaTime;
        _pointsPerSecond += Time.deltaTime * 0.1f;
        _scoreText.text = "Score: " + (int)_score;
    }

}
