using UnityEngine;
using System;
using UnityEngine.EventSystems;

public enum SwipeDirection
{
    leftSwipe,
    rightSwipe, 
    upSwipe,
    downSwipe,
    noSwipe
}
public class SwipeController : MonoBehaviour, InputManager, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startTouch = Vector2.zero;

    private Vector2 _difference;
    public event Action isJumping;
    public event Action isRolling;

    public Side MovementInput()
    {
        if(Math.Abs(_difference.x) >Math.Abs(_difference.y))
        {
            if (_difference.x < 0)
            {
                _difference = Vector2.zero;
                return Side.left;
            }
            if (_difference.x > 0)
            {
                _difference = Vector2.zero;
                return Side.right;
            }
        }
        return Side.middle;
    }

    public void CheckInput()
    {
        Debug.Log(_difference);
        if(_difference.y < _difference.x)
        {
            if (_difference.y < 0)
            {
                _difference = Vector2.zero;
                isRolling?.Invoke();
            }
            if (_difference.y > 0)
            {
                _difference = Vector2.zero;
                isJumping?.Invoke();
            }
        }    
    }

    private SwipeDirection DefineSwipe()
    {
        if (_difference.y > _difference.x)
        {

            if (_difference.y < 0)
                return SwipeDirection.downSwipe;
            else if (_difference.y > 0)
                return SwipeDirection.upSwipe;
        }
        else
        {
            if (_difference.x < 0)
                return SwipeDirection.leftSwipe;
            else if (_difference.x > 0)
                return SwipeDirection.rightSwipe;
        }
        return SwipeDirection.noSwipe;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 delta = data.position - _startTouch;
        _difference = delta;
        _startTouch = data.position;
        Debug.Log(_difference);
        /*
        Debug.Log("a");
        _difference = new Vector2(data.pressPosition.x - data.position.x, data.pressPosition.y - data.position.y);
        Debug.Log(data.pressPosition.x - data.position.x);
        //DefineSwipe();
        */
    }
    public void OnEndDrag(PointerEventData data)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startTouch = eventData.position;
    }
}
