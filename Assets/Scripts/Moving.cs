using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { left, middle, right }

public class Moving : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Collision _collision;

    private InputManager _inputType;

    private Side _side = Side.middle;
    private float _newXPos = 0f;
    private float _xValue = 3;
    private float _dodgeSpeed = 9;
    private float x, y;

    private bool _isJumping = false;
    private bool _isRolling;
    private float _jumpPower = 10;

    private float _colHeight;
    private float _colCenterY;
    private float _rollCounter;

    private float _forwardSpeed = 7;

    private bool _isDead = false;

    private void Awake()
    {
#if UNITY_EDITOR
        SetStrategy(new InputKeyboard());
#endif

        _colHeight = _characterController.height;
        _colCenterY = _characterController.center.y;
    }

    private void OnEnable()
    {
        _collision.isDead += Die;
        _inputType.isJumping += Jump;
        _inputType.isRolling += Roll;
    }

    private void OnDisable()
    {
        _collision.isDead -= Die;
        _inputType.isJumping -= Jump;
        _inputType.isRolling -= Roll;
    }

    private void FixedUpdate()
    {
        if(!_isDead)
        Move();
    }

    private void SetStrategy(InputManager inputType)
    {
        Debug.Log("input type set" + inputType.ToString());
        _inputType = inputType;
    }

    private void Jump()
    {
        if (_characterController.isGrounded && !_isJumping)
        {
            y = _jumpPower;
            _isJumping = true;
        }
    }

    private void Roll()
    {
        _rollCounter = Time.deltaTime;
        if(_rollCounter <=0f)
        {
            _rollCounter = 0f;
            _characterController.center = new Vector3(0, _colCenterY, 0);
            _characterController.height = _colHeight;
            _isRolling = false;
        }
        _rollCounter = 0.2f;
        y -= 10f;
        _characterController.center = new Vector3(0, _colCenterY/4, 0);
        _characterController.height = _colHeight/4;
        _isRolling = true;
        _isJumping = false;
    }

    private void NormalizationVertical()
    {
        if (!_characterController.isGrounded)
        {
            y -= _jumpPower * 2 * Time.deltaTime;
            _isJumping = false;
        }
        _rollCounter -= Time.deltaTime/2;
        if (_rollCounter <= 0f)
        {
            _rollCounter = 0f;
            _characterController.center = new Vector3(0, _colCenterY, 0);
            _characterController.height = _colHeight;
            _isRolling = false;
        }
    }

    private void HorizontalMovement()
    {
        _inputType.MovementInput();
        Side inputSide = _inputType.MovementInput();
        if (inputSide == Side.left)
        {
            if (_side == Side.middle)
            {
                _newXPos = -_xValue;
                _side = Side.left;
            }
            else if (_side == Side.right)
            {
                _newXPos = 0;
                _side = Side.middle;
            }
        }
        else if (inputSide == Side.right)
        {
            if (_side == Side.middle)
            {
                _newXPos = _xValue;
                _side = Side.right;
            }
            else if (_side == Side.left)
            {
                _newXPos = 0;
                _side = Side.middle;
            }
        }
    }

    public void Move()
    {
        NormalizationVertical();
        HorizontalMovement();
        _inputType.CheckInput();
        Vector3 moveVector = new Vector3((x - transform.position.x), y * Time.deltaTime, _forwardSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, _newXPos, Time.deltaTime * _dodgeSpeed);
        _characterController.Move(moveVector);
    }

    private void Die()
    {
        _isDead = true;
    }
}
