using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Moving moving;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private SwipeController swipeController;
    

    private void Awake()
    {
        transform.position = new Vector3(0,1.2f,0);
    }

    private void FixedUpdate()
    {
        moving.Move();
    }
}
