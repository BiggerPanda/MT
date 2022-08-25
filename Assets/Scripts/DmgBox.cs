using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DmgBox : MonoBehaviour
{
    private bool _isTouching;
    private float _touchTime = 1f;
    private float _touchDuration;

    private GameObject _objectTouching;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _objectTouching = other.gameObject;
            other.gameObject.GetComponent<Health>().TakeDamage(10);
            _isTouching = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _objectTouching = null;
            _isTouching = false;
        }
    }

    void Update()
    {
        if (_isTouching)
        {
            _touchDuration += Time.deltaTime;
            if (_touchDuration >= _touchTime)
            {
                _objectTouching.gameObject.GetComponent<Health>().TakeDamage(10);
                _touchDuration = 0f;
            }
        }
    }

    
}
