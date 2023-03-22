using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Move : MonoBehaviour
{
    private Animator _playerAnimator;
    
    private Rigidbody rigidbody;
    
    public GameObject camera;
    
    public float moveSpeed;
    public float rotSpeed;
    
    private Vector3 dir = Vector3.zero;

    public LayerMask layer;

    //private Vector3 camPlayer;
    
    private void Start()
    {
        //camPlayer = camera.transform.position - this.transform.position;
        rigidbody = this.GetComponent<Rigidbody>();
        _playerAnimator = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //camera.transform.position = this.transform.position + camPlayer;
        
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();
        
        // _playerAnimator.Parameter변경
        _playerAnimator.SetFloat("ForwardVelocity", dir.magnitude);
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
}