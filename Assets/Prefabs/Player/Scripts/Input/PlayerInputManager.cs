using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] MovementController _movementController;
    [SerializeField] MouseController _mouseController;
    [SerializeField] bool _toggleInput = false;
    private PlayerInputActions _inputActions;
    private PlayerInputActions.GroundMovementActions _groundMovementActions;

    private Vector2 _movementInput;
    private Vector2 _mouseInput;
    private Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _groundMovementActions = _inputActions.GroundMovement;

        _groundMovementActions.HorizontalMovement.performed += ctx =>
            _movementInput = ctx.ReadValue<Vector2>();

        _groundMovementActions.Jump.performed += _ =>
            _movementController.Jump();


        _groundMovementActions.MouseX.performed += ctx =>
            _mouseInput.x = ctx.ReadValue<float>();

        _groundMovementActions.MouseY.performed += ctx =>
            _mouseInput.y = ctx.ReadValue<float>();

        _groundMovementActions.Crouch.performed += _ =>
            _movementController.Crouch();

        _groundMovementActions.Sprint.performed += _ =>
            _movementController.Sprint();
        if (!_toggleInput)
        {
            _groundMovementActions.Sprint.canceled += _ =>
               _movementController.Sprint();
            _groundMovementActions.Crouch.canceled += _ =>
                _movementController.Crouch();
        }

        _groundMovementActions.MousePosition.performed += ctx =>
            _mousePosition = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        _movementController.ReadInput(_movementInput);
        _mouseController.ReadInput(_mouseInput);
        _mouseController.ReadMousePosition(_mousePosition);
    }

    [ContextMenu("Toggle Input")]
    private void SwichToggleInput()
    {   
        _toggleInput = !_toggleInput;
        if (!_toggleInput)
        {
            _groundMovementActions.Sprint.canceled += _ =>
                _movementController.Sprint();
            _groundMovementActions.Crouch.canceled += _ =>
                _movementController.Crouch();
        }
        else
        {
            _groundMovementActions.Sprint.canceled -= _ =>
                _movementController.Sprint();
            _groundMovementActions.Crouch.canceled -= _ =>
                _movementController.Crouch();
        }
    }
}
