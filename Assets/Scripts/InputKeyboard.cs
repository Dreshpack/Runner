using UnityEngine;
using System;

public class InputKeyboard : InputManager
{
    public event Action leftMove;
    public event Action rightMove;
    public event Action isJumping;
    public event Action isRolling;

    public Side MovementInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        return Side.left;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            return Side.right;

        return Side.middle;
    }

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isRolling?.Invoke();
        }
    }

}
