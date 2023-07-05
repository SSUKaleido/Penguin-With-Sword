using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    [SerializeField]
    private GameObject Model;
    private Animator ModelAnimator;

    [SerializeField]
    private GameObject PlayerCollider;

    [SerializeField]
    private bool isButtonPressing=false;
    
    private void Awake()
    {
        ModelAnimator=Model.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GetNowInteractionObject()
    {
        ;
    }
}
