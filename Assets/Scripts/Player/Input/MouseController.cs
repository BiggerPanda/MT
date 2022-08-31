using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller
{
    [SerializeField] private GameObject _playerHeadRotateTarget;
    private float pitch, yaw = 0;
    private float _mouseSensitivity = 0.1f;
    private Vector2 _mouseInput;
     private RaycastHit _pickUpRaycast;

    public override void ReadInput(Vector2 input)
    {
        _mouseInput = input;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Look()
    {
        pitch -= _mouseInput.y * _mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -90, 60);
        yaw += _mouseInput.x * _mouseSensitivity;
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        _rigidbody.rotation = Quaternion.Euler(0, yaw, 0);
    }

    public void PickUp()
    {
        Debug.Log("PickUp");
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2, Color.red,2);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _pickUpRaycast, 3f))
        {
            if (_pickUpRaycast.collider.gameObject.layer == LayerMask.NameToLayer("Pickable"))
            {
                _pickUpRaycast.collider.gameObject.GetComponent<ItemObject>().PickUp();
                Debug.Log("Picked up");
            }
        }

    }

    private void Update()
    {
        Look();
    }
}
