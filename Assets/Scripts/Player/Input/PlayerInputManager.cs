using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MT.Inventory;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] MovementController _movementController;
    [SerializeField] MouseController _mouseController;
    [SerializeField] bool _toggleInput = false;
    private InventoryController _inventoryController;
    private PlayerInputActions _inputActions;
    private PlayerInputActions.GroundMovementActions _groundMovementActions;
    private PlayerInputActions.InteractionsActions _interactionsActions;
    private PlayerInputActions.InventoryActions _inventoryActions;

    private Vector2 _movementInput;
    private Vector2 _mouseInput;
    private Vector2 _mousePosition;

    [Inject]
    public void Construct(InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
    }
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
        _interactionsActions = _inputActions.Interactions;
        _inventoryActions = _inputActions.Inventory;

        _groundMovementActions.HorizontalMovement.performed += ctx =>
            _movementInput = ctx.ReadValue<Vector2>();

        _groundMovementActions.Jump.started += _ =>
            _movementController.Jump();


         _groundMovementActions.MouseX.performed += ctx =>
             _mouseInput.x = ctx.ReadValue<float>();

         _groundMovementActions.MouseY.performed += ctx =>
            _mouseInput.y = ctx.ReadValue<float>();

        _groundMovementActions.Crouch.performed += _ =>
            _movementController.Crouch();

        _groundMovementActions.Sprint.performed += _ =>
            _movementController.Sprint();


        _groundMovementActions.MousePosition.performed += ctx =>
            _mousePosition = ctx.ReadValue<Vector2>();

        _interactionsActions.PickUp.performed += _ =>
            _mouseController.PickUp();

        _inventoryActions.OpenInventory.performed += _ =>
            _inventoryController.ToggleInventory();

        _inventoryActions.LeftButtonClick.performed += _ =>
            _inventoryController.OnMouseClick();    
    }

    private void Update()
    {
        _mouseController.ReadInput(_mouseInput);
        _movementController.ReadInput(_movementInput);
        _inventoryController.ReadInput(_mousePosition);
    }

}
