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

    public GameManager _gameManager;
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
                // TODO: 실패 연출
                _gameManager.ReduceHeart();
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
    }
    
    public void CaptainAwake()
    {
        if (isSleeping)
        {
            _gameManager.AddStageScore();
            PlayerPrefs.SetInt("captainAwakeCount",PlayerPrefs.GetInt("captainAwakeCount")+1);
            isSleeping = false;
            _captainAnimator.SetBool("isSleeping",false);
        }
        _wakeTime = 0f;
    }
}
