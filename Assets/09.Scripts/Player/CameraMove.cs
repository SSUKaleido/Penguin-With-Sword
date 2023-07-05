using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //테스트 시 비활성화 해둠
    //public GameObject camera;
    
    private Vector3 camPlayer;
    
    private void Start()
    {
        camPlayer = GetComponent<Camera>().transform.position - this.transform.position;
    }

    void Update()
    {
        //테스트 시 비활성화 해둠
        //camera.transform.position = this.transform.position + camPlayer;
    }
}