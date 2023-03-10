using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Collision _collision;
    [SerializeField] private Pause _pauseScript;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private RevivalAds _revivalAds;

    private float _score;
    private float _pointsPerSecond = 1;
    private bool _isDead = false;
    private bool _paused = false;

    private void OnEnable()
    {
        _collision.isDead += Die;
        _pauseScript._pauseGame += PauseCounting;
        _pauseScript._continueGame += ContinueCounting;
        _revivalAds.played += Revive;
    }

    private void OnDisable()
    {
        _collision.isDead -= Die;
        _pauseScript._pauseGame -= PauseCounting;
        _pauseScript._continueGame -= ContinueCounting;
        _revivalAds.played -= Revive;
    }

    private void Die()
    { 
        _dataBase.SetScore(PlayerPrefs.GetString("Name"), (int)_score);
        _isDead = true;
    }

    private void Revive()
    {
        _isDead = false;
    }    

    private void PauseCounting()
    {
        _paused = true;
    }

    private void ContinueCounting()
    {
        _paused = false;
    }

    private void UpdateScore()
    {
        _score += _pointsPerSecond * Time.deltaTime;
        _pointsPerSecond += Time.deltaTime * 0.1f;
    }
    private void DisplayText()
    {
        _scoreText.text = "Score: " + (int)_score;
    }
    private void FixedUpdate()
    {
        if(!_isDead && !_paused)
        UpdateScore();
        DisplayText();
    }
}
