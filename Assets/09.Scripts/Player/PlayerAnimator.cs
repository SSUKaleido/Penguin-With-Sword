using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // _playerAnimator.Parameter변경
        //_playerAnimator.SetFloat("ForwardVelocity", dir.magnitude);

        if (Input.GetKeyDown("r"))
        {
            _animator.SetTrigger("Smash");
        }
    }

    public void SetFloat(string paramName, float value)
    {
        _animator.SetFloat(paramName, value);
    }
    
    public void SetBool(string paramName, bool value)
    {
        _animator.SetBool(paramName, value);
    }
}
