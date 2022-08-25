using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] Controller _movementController;
    [SerializeField] Controller _mouseController;
    private PlayerInputActions _inputActions;
    private PlayerInputActions.GroundMovementActions _groundMovementActions;

    private Vector2 _movementInput;
    private Vector2 _mouseInput;

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
            _movementController.ReadInput(true);


        _groundMovementActions.MouseX.performed += ctx =>
            _mouseInput.x = ctx.ReadValue<float>();

        _groundMovementActions.MouseY.performed += ctx =>
            _mouseInput.y = ctx.ReadValue<float>();

    }

    private void Update()
    {
        _movementController.ReadInput(_movementInput);
        _mouseController.ReadInput(_mouseInput);
    }
}
