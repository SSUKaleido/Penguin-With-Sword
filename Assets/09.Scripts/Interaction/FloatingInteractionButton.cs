using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class FloatingInteractionButton : MonoBehaviour
{
    [HideInInspector]
    public RectTransform RectTransform;
    public RectTransform InnerButton;
    
    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}
