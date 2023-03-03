using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Collision _collision;

    private float _score;
    private float _pointsPerSecond = 1;
    private bool _isDead = false;

    private void OnEnable()
    {
        _collision.isDead += StopCounting;
    }

    private void StopCounting()
    {
        _isDead = true;
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
        if(!_isDead)
        UpdateScore();
        DisplayText();
    }
}
