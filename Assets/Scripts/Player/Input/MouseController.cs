using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller
{
    [SerializeField] private GameObject _playerHeadRotateTarget;
    private float pitch, yaw = 0;
    private float _mouseSensitivity = 0.1f;
    private Vector2 _mouseInput;

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

    private void Update()
    {
        Look();
    }
}
