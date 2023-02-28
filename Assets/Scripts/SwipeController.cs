using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public bool _tap, _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
    private bool _isDragging = false;
    private Vector2 _startTouch, _swipeDelta;

    private void FixedUpdate()
    {
        SwipeControll();
    }
    
    private void SwipeControll()
    {
        _tap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _swipeLeft = true;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            _swipeRight = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            _isDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }
        if (Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
            _swipeDelta = Vector2.zero;
            if(_isDragging)
            {
                if(Input.touches.Length < 0)
                {
                    _swipeDelta = (Vector2)Input.touches[0].position - _startTouch;
                }
                else if(Input.GetMouseButton(0))
                {
                    _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
                }


                if(_swipeDelta.magnitude > 100)
                {
                    float x = _swipeDelta.x;
                    float y = _swipeDelta.y;
                    if(Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if(x < 0)
                        {
                            _swipeLeft = true;
                        }
                        else
                        {
                            _swipeRight = true;
                        }
                    }
                    else
                    {
                        if(y < 0)
                        {
                            _swipeDown = true;
                        }
                        else
                        {
                            _swipeUp = true;
                        }
                    }
                }
            }
        }
    }
    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDragging = false;
    }


}
