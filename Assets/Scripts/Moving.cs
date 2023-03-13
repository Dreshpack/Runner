using UnityEngine;

public enum Side { left, middle, right }

public class Moving : MonoBehaviour
{
    [SerializeField] private SwipeController _swipe;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnimationManager _animManager;

    #region Fields
    private InputManager _inputType;

    private Side _side = Side.middle;
    private float _newXPos = 0f;
    private float _xValue = 3;
    private float _dodgeSpeed = 9;
    private float x, y;

   
    private float _jumpPower = 7;

    private float _colHeight;
    private float _colCenterY;
    private float _rollCounter;

    private float _forwardSpeed = 0;

    private bool _isDead = false;
    #endregion

    private void Awake()
    {
#if UNITY_EDITOR
        SetStrategy(new InputKeyboard());
        //SetStrategy(_swipe);
#endif
#if UNITY_ANDROID
        SetStrategy(_swipe);
        //SetStrategy(new SwipeController());
#endif
        _colHeight = _characterController.height;
        _colCenterY = _characterController.center.y;
    }

    private void OnEnable()
    {
        _inputType.isJumping += Jump;
        _inputType.isRolling += Roll;
        _inputType.leftMove += LeftMove;
        _inputType.rightMove += RightMove;
    }

    private void OnDisable()
    {
        _inputType.isJumping -= Jump;
        _inputType.isRolling -= Roll;
        _inputType.leftMove -= LeftMove;
        _inputType.rightMove -= RightMove;
    }

    private void FixedUpdate()
    {
        Running();
    }

    private void Running()
    {
        if (!_isDead && _forwardSpeed > 0)
        {
            Move();
            IncreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        if (_forwardSpeed < 50)
            _forwardSpeed += Time.deltaTime * 0.1f;
    }

    public void StartRunning()
    {
        _forwardSpeed = 7;
        _animManager.Run();
    }

    private void SetStrategy(InputManager inputType)
    {
        Debug.Log("input type set" + inputType.ToString());
        _inputType = inputType;
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            y = _jumpPower;
            _animManager.Jump();
        }
    }

    private void Roll()
    {
        _rollCounter = Time.deltaTime;
        if (_rollCounter <= 0f)
        {
            _rollCounter = 0f;
            _characterController.center = new Vector3(0, -0.5f, 0);
            _characterController.height = _colHeight;
        }
        _rollCounter = 0.5f;
        y -= 10f;
        _characterController.center = new Vector3(0, -0.5f, 0);
        _characterController.height = _colHeight / 4;
        _animManager.Roll();
    }

    private void RightMove()
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

    private void LeftMove()
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

    public void Move()
    {
        NormalizationVertical();
        Vector3 moveVector = new Vector3((x - transform.position.x), y * Time.deltaTime, _forwardSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, _newXPos, Time.deltaTime * _dodgeSpeed);
        _characterController.Move(moveVector);
    }

    private void NormalizationVertical()
    {
        if (!_characterController.isGrounded)
        {
            y -= _jumpPower * 2 * Time.deltaTime;
        }
        _rollCounter -= Time.deltaTime / 2;
        if (_rollCounter <= 0f)
        {
            _rollCounter = 0f;
            _characterController.center = new Vector3(0, _colCenterY, 0);
            _characterController.height = _colHeight;
        }
    }

    public void Die()
    {
        _isDead = true;
        _animManager.Die();
    }

    public void Revive()
    {
        _isDead = false;
        _animManager.Revive();
    }

}
