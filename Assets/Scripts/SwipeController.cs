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
    public event Action leftMove;
    public event Action rightMove;

    public Side MovementInput()
    {
        if(Math.Abs(_difference.x) > Math.Abs(_difference.y))
        {
            if (_difference.x < 0)
            {
                leftMove?.Invoke();
                return Side.left;
            }
            if (_difference.x > 0)
            {
                rightMove?.Invoke();
                return Side.right;
            }
        }
        return Side.middle;
    }

    public void CheckInput()
    {
        if(Math.Abs(_difference.y) >Math.Abs(_difference.x))
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
    
    public void OnDrag(PointerEventData data)
    {
        Vector2 delta = data.position - _startTouch;
        _difference = delta;
        _startTouch = data.position;
    }
    public void OnEndDrag(PointerEventData data)
    {
        MovementInput();
        CheckInput();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startTouch = eventData.position;
        
    }
}
