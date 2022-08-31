using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public enum MovementState
{
    Idle,
    Walking,  // 0
    Running,  // 1
    Crouching, // 2
    Stairs, // 3
    Falling, // 4

}

public class MovementController : Controller
{
    [ReadOnly]
    public MovementState movementState;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 11f;
    [SerializeField] private float _crouchSpeed = 2f;
    [SerializeField] private float _groundDrag = 0.5f;
    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;
    [Header("Ground Check")]
    [SerializeField] private float _height = 2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool grounded;
    [Header("Slope Handle")]
    [SerializeField] private float _slopeLimit = 45f;
    [SerializeField] private float _stepOffset = 0.5f;
    [Header("Stairs")]
    [SerializeField] private float _starirsMinDepth = 0.3f;
    [SerializeField] private float _maxStepHeight = 0.3f;

    private Vector2 _movementInput;
    private Vector3 _movementDirection;
    private float _movementSpeed;
    private bool _isSprinting, _isCrouching;
    private RaycastHit _slopeHit;
    private RaycastHit _stairsHit;

    private bool _exitSlope;

    public override void ReadInput(Vector2 input)
    {
        _movementInput = input;
    }
    #region Movement
    public void Jump()
    {
        _exitSlope = true;
        if (!grounded)
            return;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.up * _jumpForce * _rigidbody.mass, ForceMode.Impulse);
        Invoke("ResetJump", 0.1f);
    }
    private void ResetJump()
    {
        _exitSlope = false;
    }

    public void Crouch()
    {
        _isCrouching = !_isCrouching;
        if (_isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            _movementSpeed = _crouchSpeed;
        }
        else
        {
            _movementSpeed = _walkSpeed;
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }

    }

    public void Sprint()
    {
        _isSprinting = !_isSprinting;
        _movementSpeed = _isSprinting ? _runSpeed : _walkSpeed;
    }

    private void Movement()
    {
        _movementDirection = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        if (IsOnSlope() && !_exitSlope)
        {
            _rigidbody.AddForce(SlopeMoveDirection() * _movementSpeed * _rigidbody.mass * 10f, ForceMode.Force);
            if (_rigidbody.velocity.y > 0f)
                _rigidbody.AddForce(Vector3.down * _rigidbody.mass * 10f, ForceMode.Force);
        }
        else if (grounded)
            _rigidbody.AddForce(_movementDirection * _movementSpeed * _rigidbody.mass * 10f, ForceMode.Force);
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, _height * 0.5f + 0.2f, _groundLayer);
    }

    private bool IsOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _height * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle <= _slopeLimit && angle != 0;
        }
        return false;
    }
    private Vector3 SlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_movementDirection, _slopeHit.normal).normalized;
    }

    #endregion
    #region Handle Input
    private void DragHangle()
    {
        if (grounded)
        {
            _rigidbody.drag = _groundDrag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }
    private void SpeedHandle()
    {
        if (IsOnSlope() && !_exitSlope)
        {
            if (_rigidbody.velocity.magnitude > _movementSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _movementSpeed;
            }
        }
        else
        {
            Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            if (velocity.magnitude > _movementSpeed)
            {
                _rigidbody.velocity = new Vector3(velocity.normalized.x * _movementSpeed,
                                                  _rigidbody.velocity.y,
                                                   velocity.normalized.z * _movementSpeed);
            }
        }

    }
    private void HandleState()
    {

        if (_isCrouching)
        {
            movementState = MovementState.Crouching;
        }
        else if (_isSprinting && grounded && _movementInput != Vector2.zero)
        {
            movementState = MovementState.Running;

        }
        else if ((grounded || IsOnSlope()) && _movementInput != Vector2.zero)
        {
            movementState = MovementState.Walking;
        }
        else
        {
            movementState = MovementState.Idle;
        }
    }
    private void HandleGravity()
    {
        _rigidbody.useGravity = !IsOnSlope();
    }
    private void Handler()
    {
        DragHangle();
        SpeedHandle();
        HandleState();
        HandleGravity();
    }
    #endregion
    private void Update()
    {
        GroundCheck();
        Handler();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Start()
    {
        _movementSpeed = _walkSpeed;
    }
}
