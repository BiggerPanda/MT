using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : Controller
{
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
    
    private Vector2 _movementInput;
    private Vector3 _movementDirection;
    private float _movementSpeed;
    private bool _isSprinting, _isCrouching;
    public override void ReadInput(Vector2 input)
    {
        _movementInput = input;
    }
#region Movement
    public void Jump()
    {
        if(!grounded)
            return;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.up * _jumpForce* _rigidbody.mass, ForceMode.Impulse);
    }

    public void Crouch()
    {
        _isCrouching = !_isCrouching;
        if(_isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f , transform.localScale.z);
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
        if(grounded) 
           _rigidbody.AddForce(_movementDirection * _movementSpeed *_rigidbody.mass * 10f , ForceMode.Force);
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, _height*0.5f + 0.1f , _groundLayer);
    } 
#endregion 
#region Handle Input
    private void DragHangle()
    {
        if(grounded)
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
        Vector3 velocity = new Vector3(_rigidbody.velocity.x , 0 , _rigidbody.velocity.z);
        if(velocity.magnitude > _movementSpeed)
        {
            _rigidbody.velocity =  new Vector3(velocity.normalized.x * _movementSpeed,
                                              _rigidbody.velocity.y ,
                                               velocity.normalized.z * _movementSpeed);
        }
    }
#endregion
    private void Update()
    {
        GroundCheck();
        DragHangle();
        SpeedHandle();
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
