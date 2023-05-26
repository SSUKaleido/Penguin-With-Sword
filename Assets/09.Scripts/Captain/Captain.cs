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
    
    void Start()
    {
        //애니메이터 불러오기
        _captainAnimator = GetComponentInChildren<Animator>();
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
                // TODO: 1회 실패 연출 및 생명력 감소
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
        return;
    }
    public void CaptainAwake()
    {
        _wakeTime = 0f;
        isSleeping = false;
        _captainAnimator.SetBool("isSleeping",false);
        return;
    }
}
