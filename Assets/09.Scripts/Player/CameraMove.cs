using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject camera;
    private Vector3 camPlayer;
    //private Vector3 dir = Vector3.zero;
    //public LayerMask layer;
    
    private void Start()
    {
        camPlayer = camera.transform.position - this.transform.position;
    }

    void Update()
    {
        camera.transform.position = this.transform.position + camPlayer;
    }
}