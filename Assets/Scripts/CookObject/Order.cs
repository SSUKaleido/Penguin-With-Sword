﻿using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace CookObject
{
    public class Order : MonoBehaviour
    {
        private OrderData _orderData;
        private const float AlertLimitTime = 10f;
        
        public bool IsDelivered { get; private set; }
        public float RemainingTime { get; private set; }
        public float ArrivalTime { get; private set; }
        
        private const float BaseInitialTime = 60f;
        public float InitialRemainingTime { get; private set; } = BaseInitialTime;

        public OrderData OrderData => _orderData;
        public List<ObjectData> Ingredients => _orderData.objects;
        public float TimeRemainingWhenDelivered { get; private set; }

        private Coroutine _countdownCoroutine;
        
        public delegate void Expired(Order order);
        public event Expired OnExpired;
                
        public delegate void Delivered(Order order);
        public event Delivered OnDelivered;
        
        public delegate void AlertTime(Order order);
        public event AlertTime OnAlertTime;

        public delegate void UpdatedCountdown(float remainingTime);
        public event UpdatedCountdown OnUpdatedCountdown;

        public void Setup(OrderData orderData, float additionalTime)
        {
            IsDelivered = false;
            _orderData = orderData;
            ArrivalTime = Time.time;
            InitialRemainingTime += BaseInitialTime + additionalTime;
            SetCountdownTime(InitialRemainingTime);
            StartCountdown();
        }

        private void SetCountdownTime(float seconds)
        {
            InitialRemainingTime = seconds;
            RemainingTime = InitialRemainingTime;
        }

        private void StartCountdown()
        {
            _countdownCoroutine = StartCoroutine(CountdownCoroutine());
        }

        private void StopCountdown()
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(CountdownCoroutine());
            }
        }

        private void ResumeCountdown()
        {
            if (_countdownCoroutine != null)
            {
                StartCoroutine(CountdownCoroutine());
            }   
        }

        private void ResetCountdown()
        {
            ArrivalTime = Time.time;
            RemainingTime = BaseInitialTime;
            StopCoroutine(CountdownCoroutine());
            StartCountdown();
        }

        private IEnumerator CountdownCoroutine()
        {
            while (RemainingTime > AlertLimitTime)
            {
                RemainingTime -= Time.deltaTime;
                OnUpdatedCountdown?.Invoke(RemainingTime);
                yield return null;
            }
            
            OnAlertTime?.Invoke(this);
            
            while (RemainingTime > 0)
            {
                RemainingTime -= Time.deltaTime;
                OnUpdatedCountdown?.Invoke(RemainingTime);
                yield return null;
            }
            
            OnExpired?.Invoke(this);
            ResetCountdown();
            ResetCountdown();
        }

        public void SetOrderDelivered()
        {
            TimeRemainingWhenDelivered = RemainingTime;
            IsDelivered = true;
            StopCountdown();
            OnDelivered?.Invoke(this);
        }
        
    }
}