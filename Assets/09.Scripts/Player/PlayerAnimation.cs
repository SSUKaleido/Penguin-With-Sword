using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject camera;
    private Vector3 camPlayer;

    private Animator _playerAnimator;
    
    //private Vector3 dir = Vector3.zero;
    //public LayerMask layer;
    
    private void Start()
    {
        camPlayer = camera.transform.position - this.transform.position;
        _playerAnimator = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        camera.transform.position = this.transform.position + camPlayer;
        
        // _playerAnimator.Parameter변경
        //_playerAnimator.SetFloat("ForwardVelocity", dir.magnitude);

        if (Input.GetKeyDown("r"))
        {
            _playerAnimator.SetTrigger("Smash");
        }
    }
}