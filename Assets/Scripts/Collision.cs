using UnityEngine;
using System;

public class Collision : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    public event Action isDead;
    private bool _isDead = false;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Border" && !_isDead)
        {
            _isDead = true;
            isDead?.Invoke();
        }
    }
}
