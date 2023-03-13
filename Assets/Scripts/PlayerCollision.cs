using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    private bool _isDead = false;

    public GameObject CurrentBorder { get; private set; }
    public event Action isDead;


    public void Revive()
    {
        _isDead = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Border" && !_isDead)
        {
            CurrentBorder = hit.gameObject;
            _isDead = true;
            Debug.Log(hit.gameObject);
            isDead?.Invoke();
        }
    }
}
