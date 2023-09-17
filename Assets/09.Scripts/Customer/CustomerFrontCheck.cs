using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFrontCheck : MonoBehaviour
{
    private Transform _transform;

    private CustomerMovement _customerMovement;

    private float originSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _customerMovement = (CustomerMovement)_transform.parent.GetComponent<CustomerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("OnTriggerEnter : " + other.tag);
        if (other.CompareTag("Customer"))
        {
            // Debug.Log("SetSpeed : " + other.tag);
            originSpeed = _customerMovement.GetSpeed();
            _customerMovement.SetSpeed(0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            _customerMovement.SetSpeed(originSpeed);
        }
    }
}