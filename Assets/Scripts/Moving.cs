using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Side { left, middle, right }

public class Moving : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _meshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private SwipeController _swipe;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Collision _collision;
    [SerializeField] private Pause _pauseScript;
    [SerializeField] private StartGame _startGame;
    [SerializeField] private RevivalAds _revivalAds;
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private Panel _losePanel;

    private InputManager _inputType;

    private Side _side = Side.middle;
    private float _newXPos = 0f;
    private float _xValue = 3;
    private float _dodgeSpeed = 9;
    private float x, y;

    private bool _isJumping = false;
    private float _jumpPower = 7;

    private float _colHeight;
    private float _colCenterY;
    private float _rollCounter;

    private float _forwardSpeed = 0;
    private float _currentSpeed;

    private bool _isDead = false;

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
        _collision.isDead += Die;
        _inputType.isJumping += Jump;
        _inputType.isRolling += Roll;
        _inputType.leftMove += LeftMove;
        _inputType.rightMove += RightMove;
        _pauseScript._pauseGame += PauseMovement;
        _pauseScript._continueGame += ContinueGame;
        _startGame.isStarted += StartRunning;
        _revivalAds.played += Revive;
    }

    private void OnDisable()
    {
        _collision.isDead -= Die;
        _inputType.isJumping -= Jump;
        _inputType.isRolling -= Roll;
        _inputType.leftMove -= LeftMove;
        _inputType.rightMove -= RightMove;
        _pauseScript._pauseGame -= PauseMovement;
        _pauseScript._continueGame -= ContinueGame;
        _startGame.isStarted -= StartRunning;
        _revivalAds.played += Revive;
    }

    private void FixedUpdate()
    {
        Running();
    }

    private void IncreaseSpeed()
    {
        if (_forwardSpeed < 50)
            _forwardSpeed += Time.deltaTime * 0.1f;
    }

    private void StartRunning()
    {
        _forwardSpeed = 7;
        _animator.SetFloat("speed", 1f);
    }

    private void Running()
    {
        if (!_isDead && _forwardSpeed > 0)
        {
            Move();
            IncreaseSpeed();
        }
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
            _animator.SetTrigger("jump");
            _animator.SetBool("isJumping", _isJumping);
        }
    }

    private void Roll()
    {
        _rollCounter = Time.deltaTime;
        if (_rollCounter <= 0f)
        {
            _rollCounter = 0f;
            //_characterController.center = new Vector3(0, _colCenterY, 0);
            _characterController.center = new Vector3(0, -0.5f, 0);
            _characterController.height = _colHeight;
        }
        _rollCounter = 0.5f;
        y -= 10f;
        //_characterController.center = new Vector3(0, _colCenterY / 4, 0);
        _characterController.center = new Vector3(0, -0.5f, 0);
        _characterController.height = _colHeight / 4;
        _animator.SetTrigger("slide");
        _isJumping = false;
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

    private void NormalizationVertical()
    {
        if (!_characterController.isGrounded)
        {
            y -= _jumpPower * 2 * Time.deltaTime;
            _isJumping = false;
        }
        _rollCounter -= Time.deltaTime / 2;
        if (_rollCounter <= 0f)
        {
            _rollCounter = 0f;
            _characterController.center = new Vector3(0, _colCenterY, 0);
            _characterController.height = _colHeight;
        }
    }

    public void Move()
    {
        NormalizationVertical();
        Vector3 moveVector = new Vector3((x - transform.position.x), y * Time.deltaTime, _forwardSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, _newXPos, Time.deltaTime * _dodgeSpeed);
        _characterController.Move(moveVector);
    }

    private void PauseMovement()
    {
        _currentSpeed = _forwardSpeed;
        _forwardSpeed = 0;
        _animator.SetFloat("speed", _forwardSpeed);
        _animator.speed = 0;
    }

    private void ContinueGame()
    {
        _forwardSpeed = _currentSpeed;
        _animator.SetFloat("speed", _forwardSpeed);
        _animator.speed = 1;
    }

    private void Die()
    {
        _isDead = true;
        _losePanel.Lose();
        _animator.SetFloat("speed", 0f);
        _animator.SetBool("isDead", _isDead);
        _animator.SetTrigger("die");
    }

    private void Revive()
    {
        _isDead = false;
        _collision.Revive();
        _animator.SetBool("isDead", _isDead);
        _animator.SetFloat("speed", 1f);
        Debug.Log("Revived");
    }

}
