using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;
    private float _followingSpeed = 3f;
    private float y;

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }
    private void LateUpdate()
    {
        Vector3 followPos = _target.position + _offset;
        RaycastHit hit;
        if (Physics.Raycast(_target.position, Vector3.down, out hit, 2.5f))
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * _followingSpeed);
        else
            y = Mathf.Lerp(y, _target.position.y, Time.deltaTime * _followingSpeed);
        followPos.y = _offset.y + y;
        transform.position = followPos;
    }
}
