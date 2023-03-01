using UnityEngine;
using System;

public class InputKeyboard : InputManager
{
    public Side MovementInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        return Side.left;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            return Side.right;

        return Side.middle;
    }
    public delegate bool roll();

    public event Action isJumping;
    public event Action isRolling;
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
