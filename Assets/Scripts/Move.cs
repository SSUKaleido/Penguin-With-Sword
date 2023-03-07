using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // public CharacterController controller;
    public float moveSpeed;
    // public float turnSmoothTime;
    // float turnSmoothVelocity;
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        transform.position += movement * Time.deltaTime * moveSpeed;
        
        //회전
        /*
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir * MoveSpeed * Time.deltaTime;
        }
        */
    }
}