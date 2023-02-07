using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 dir = Vector3.zero;
    
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float rotSteed = 20.0f;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) * Mathf.Sign(dir.x) < 0 || Mathf.Sign(transform.forward.z) * Mathf.Sign(dir.z) < 0)
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSteed * Time.deltaTime);
        }
        
        _rigidbody.MovePosition(this.gameObject.transform.position + dir * (Time.deltaTime * speed) );
    }
}
