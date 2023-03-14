using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Move : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float moveSpeed;
    public float rotSpeed;
    public float jumpHeight;
    
    private Vector3 dir = Vector3.zero;

    private bool ground = false;
    public LayerMask layer;
    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();
        
        CheckGround();
        
        if (Input.GetButton("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower,ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            //지금 바라보는 방향의 부호 != 나아갈 방향 부호
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) ||
                Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0,1,0);
            }
            transform.forward = Vector3.Lerp(transform.forward,dir,rotSpeed*Time.deltaTime);
        }
        
        rigidbody.MovePosition(this.gameObject.transform.position + moveSpeed * Time.deltaTime * dir);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}