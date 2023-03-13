using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private RevivalAds _revivalAds;
    [SerializeField] private Moving _movingController;
    [SerializeField] private Panel _losePanel;
    [SerializeField] private InitManager _initManager;

    private void OnEnable()
    {
        _playerCollision.isDead += Die;
        _revivalAds.played += Revive;
        _initManager.isStarted += StartRun;
    }

    private void OnDisable()
    {
        _playerCollision.isDead -= Die;
        _revivalAds.played -= Revive;
        _initManager.isStarted -= StartRun;
    }

    private void Die()
    {
        _losePanel.Lose();
        _movingController.Die();
    }

    private void Revive()
    {
        _movingController.Revive();
        _playerCollision.Revive();
    }

    private void StartRun()
    {
        _movingController.StartRunning();
    }


}
