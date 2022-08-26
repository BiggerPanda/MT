using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Controller : MonoBehaviour
{   
    protected Rigidbody _rigidbody;
    protected Collider _collider;
     public abstract void ReadInput(Vector2 input);

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
    }
}
