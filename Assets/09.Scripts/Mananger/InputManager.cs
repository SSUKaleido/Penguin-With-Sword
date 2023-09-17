using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    //private bool isTappedInteractionButton;

    public void Awake()
    {
        //isTappedInteractionButton = false;
    }
    public void OnInteractionButton()
    {
        print("OnInteractionButton Taped");
    }
}
