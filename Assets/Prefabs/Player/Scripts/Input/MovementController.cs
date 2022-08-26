using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : Controller
{

    [Header("Stairs")]
    [SerializeField] private GameObject _stepRayLow;
    [SerializeField] private GameObject _stepRayHigh;
    [SerializeField] private float _stepHeight = 0.3f;
    [SerializeField] private float _stepSmooth = 0.01f;

    [Space]
    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _groundDrag = 0.1f;
    [Range(2, 20)] [SerializeField] private float _jumpForce = 5.0f;

    private float _playerHeight;
    private Vector2 _movementInput;
    private bool _isFalling;
    private Vector3 _velocity;
    private Vector3 _previousVelocity;

    private bool _sprinting;
    private bool _crouching;

    // Start is called before the first frame update
    void Start()
    {
        _playerHeight = GetComponent<Collider>().bounds.size.y;
        _isFalling = false;
        _velocity = Vector3.zero;
    }
    public override void ReadInput(Vector2 movementInput)
    {
        _movementInput = movementInput;
        _previousVelocity = _velocity;
    }

    public void Jump()
    {
        if (Grounded())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce * _rigidbody.mass, ForceMode.Impulse);
        }
    }

    public void Sprint()
    {
        if (!_sprinting)
        {
            _movementSpeed *= 2f;
            _sprinting = true;
        }
        else
        {
            _movementSpeed /= 2f;
            _sprinting = false;
        }
    }

    public void Crouch()
    {
        if (!_crouching)
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            _movementSpeed /= 2f;
            _crouching = true;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1f, 1);
            _movementSpeed *= 2f;
            _crouching = false;
        }
    }

    private void Move()
    {
        if (!Grounded())
            return;
        if(_movementInput.x == 0 && _movementInput.y == 0 && Grounded())
        {
            StopMovement();
            return;
        }
        _velocity = (transform.right * _movementInput.x + transform.forward * _movementInput.y);
        _rigidbody.AddForce(_velocity.normalized * _movementSpeed * _rigidbody.mass, ForceMode.Force);
    }

    public void StopMovement()
    {
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 0.1f);
    }

    private bool Grounded()
    {
        Debug.Log(Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f));
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, LayerMask.GetMask("Walkable")); ;
    }

    private bool IsOnStairs()
    {

        if (Physics.Raycast(_stepRayLow.transform.position, transform.TransformDirection(Vector3.forward), 0.1f , LayerMask.GetMask("Staris")))
        {
            if (!Physics.Raycast(_stepRayHigh.transform.position, transform.TransformDirection(Vector3.forward), 0.2f, LayerMask.GetMask("Staris")))
            {
                return true;
            }
        }
        if (Physics.Raycast(_stepRayLow.transform.position, transform.TransformDirection(1.5f, 0, 1), 0.1f, LayerMask.GetMask("Staris")))
        {
            if (!Physics.Raycast(_stepRayHigh.transform.position, transform.TransformDirection(1.5f, 0, 1), 0.2f, LayerMask.GetMask("Staris")))
            {
                return true;
            }
        }
        if (Physics.Raycast(_stepRayLow.transform.position, transform.TransformDirection(-1.5f, 0, 1), 0.1f, LayerMask.GetMask("Staris")))
        {
            if (!Physics.Raycast(_stepRayHigh.transform.position, transform.TransformDirection(-1.5f, 0, 1), 0.2f, LayerMask.GetMask("Staris")))
            {
                return true;
            }
        }

        return false;
    }


    private void StepClimb()
    {
        if (IsOnStairs())
            _rigidbody.position -= new Vector3(0, -_stepSmooth, 0);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        StepClimb();
        HandleSpeed();
        HandleDrag();
    }

    private void HandleSpeed()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > _movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _movementSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void HandleDrag()
    {
        // if (Grounded())
        //     _rigidbody.drag = _groundDrag;
        // else
        //     _rigidbody.drag = 0;
    }

}
