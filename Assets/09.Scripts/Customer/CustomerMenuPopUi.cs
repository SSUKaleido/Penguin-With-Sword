using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerMenuPopUi : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 메인카메라 쳐다보기
        transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
    }
}
