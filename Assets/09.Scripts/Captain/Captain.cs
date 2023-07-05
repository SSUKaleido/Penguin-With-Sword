using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Captain : MonoBehaviour
{
    private Transform _transform;
    
    public bool isSleeping;
    
    [SerializeField] private float dangerPeriod = 10f;
    private float _sleepingTime = 0f;

    [SerializeField] private float sleepingPeriod = 10f;
    private float _wakeTime = 0f;
    
    private Animator _captainAnimator;

    public GameManager gameManager;

    public Material awakeMaterial;
    public Material sleepMaterial;
    private SkinnedMeshRenderer captainSkinnedMeshRenderer;
    void Start()
    {   
        //애니메이터 불러오기
        _captainAnimator = GetComponentInChildren<Animator>();
        captainSkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (isSleeping)
        {
            if (_sleepingTime <= dangerPeriod)
            {
                _sleepingTime += Time.deltaTime;
            }
            else
            {
                // TODO: 실패 연출
                gameManager.ReduceHeart(transform.position);
                CaptainAwake();
            }
        }
        else
        {
            if (_wakeTime <= sleepingPeriod)
            {
                _wakeTime += Time.deltaTime;
            }
            else
            {
                CaptainSleep();
            }
        }
    }

    public void CaptainSleep()
    {
        _sleepingTime = 0f;
        isSleeping = true;
        _captainAnimator.SetBool("isSleeping",true);
        captainSkinnedMeshRenderer.material = sleepMaterial;
    }
    
    public void CaptainAwake()
    {
        if (isSleeping)
        {
            PlayerPrefs.SetInt("captainAwakeCount",PlayerPrefs.GetInt("captainAwakeCount")+1);
            isSleeping = false;
            _captainAnimator.SetTrigger("smashCaptain");
            _captainAnimator.SetBool("isSleeping",false);
            captainSkinnedMeshRenderer.material = awakeMaterial;
        }
        _wakeTime = 0f;
    }
}
