using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Collision _collision;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        _collision.isDead += Dead;
    }

    private void OnDisable()
    {
        _collision.isDead -= Dead;
    }

    private void Dead()
    {
        Debug.Log("is dead");
    }
}
