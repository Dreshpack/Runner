using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private SwipeController swipeController;
    private Vector3 _direction;
    private float _speed = 3f;
    private int _currentLine = 1;
    private int _lineDistance = 3;

    private void Update()
    {
        SwipeLimiter();
    }

    private void SwipeLimiter()
    {
        if(swipeController._swipeLeft && _currentLine > 0)
        {
            _currentLine--;
        }
        if(swipeController._swipeRight && _currentLine < 2)
        {
            _currentLine++;
        }
        Vector3 currentPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(_currentLine == 0)
        {
            currentPosition += Vector3.left * _lineDistance;
        }
        else if(_currentLine == 2)
        {
            currentPosition += Vector3.right * _lineDistance;
        }
        transform.position = currentPosition;
    }

    private void FixedUpdate()
    {
        _direction.z = _speed;
        _characterController.Move(_direction * Time.deltaTime);
    }
}
