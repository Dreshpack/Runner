using UnityEngine;
using System;

public interface InputManager 
{
    public Side MovementInput();
    public void CheckInput();

    public event Action leftMove;
    public event Action rightMove;
    public event Action isJumping;
    public event Action isRolling;
}
