using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : Controller
{
    [Range(2, 20)] [SerializeField] private float _movementSpeed = 5.0f;
    [Range(2, 20)] [SerializeField] private float _jumpForce = 5.0f;
    private Vector2 _movementInput;
    private bool _isFalling;
    private Vector3 _velocity;
    private Vector3 _previousVelocity;


    // Start is called before the first frame update
    void Start()
    {

    }
    public override void ReadInput(Vector2 movementInput)
    {
        _movementInput = movementInput;
        _previousVelocity = _velocity;
    }

    public override void ReadInput(bool jumpInput)
    {
        if (jumpInput && Grounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce * _rigidbody.mass, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        _velocity = (transform.right * _movementInput.x + transform.forward * _movementInput.y) * _movementSpeed;
        _velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _velocity;
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f);
    }

    private void LateUpdate()
    {
        if (_previousVelocity != _velocity)
        {

        }
        Move();
    }

}
