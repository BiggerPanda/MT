using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _sensitivityX = 100.0f;
    [SerializeField] private float _sensitivityY = 0.5f;
    [SerializeField] private float xClampBot = 45.0f;
    [SerializeField] private float xClampTop = -90.0f;


    private Vector2 _mouseInput;
    private Vector2 _mousePosition;
    private float _rotationX;
    private GameObject _head;

    public override void ReadInput(Vector2 mouseInput)
    {
        _mouseInput = new Vector2(mouseInput.x * _sensitivityX, mouseInput.y * _sensitivityY);
    }

    public void ReadMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _mouseInput.x * Time.deltaTime);
        
        //restrict rotation of head to body  
        _rotationX -= _mouseInput.y;
        _rotationX = Mathf.Clamp(_rotationX,xClampBot,xClampTop);
        Vector3 playerRotation = transform.eulerAngles;
        playerRotation.x = _rotationX;
        _head.transform.eulerAngles = playerRotation;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _head = _animator.GetBoneTransform(HumanBodyBones.Head).gameObject;
    }
}
