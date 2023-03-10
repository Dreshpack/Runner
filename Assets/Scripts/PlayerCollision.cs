using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] RevivalAds _revivalAds;

    public GameObject currentBorder;
    public event Action isDead;
    public delegate void getBorder(GameObject border);
    public event getBorder getHit;
    private bool _isDead = false;

    public void Revive()
    {
        Debug.Log("start collision");
        _isDead = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Border" && !_isDead)
        {
            currentBorder = hit.gameObject;
            _isDead = true;
            Debug.Log(hit.gameObject);
            getHit?.Invoke(currentBorder);
            isDead?.Invoke();
        }
    }
}
