using UnityEngine;
using System;

public class Collision : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    public event Action isDead; 

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Border")
        {
            isDead?.Invoke();
        }
    }
}
