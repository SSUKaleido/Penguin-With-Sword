using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 생선에 붙는 컴포넌트
public class Fish : MonoBehaviour
{
    //변수
    public bool cookable;
    
    /**
     * cookState: 원하는 생선 종류
     * comment: 손님이 원하는 요리정도를 비교하기 위함
     * code 0: 날생선 상태
     * code 1: 잘익은 상태
     * code 2: 탄 상태
     */
    public int cookState = 0;
    
    private float cookTime = 0.0f;

    public float wellcookTime = 10f;
    public float overcookTime = 20f;
    
    void Update()
    {
        // update안에서 if Pan 위면 Time.deltaTime을 조리시간에 +=
        if( cookable /*TODO: Pan 위에 있는지 판단코드 필요*/){
            cookTime += Time.deltaTime;
        }

        // 조리된 총 시간 합이 생선 종류 경계 일때마다
        // 시간 조건에 따라서 Material을 바꾸는 함수를 호출
        if (cookTime > wellcookTime && cookTime < overcookTime ){ 
            // 잘익은 시간 < cookTime < 타는 시간
            // material을 "잘익은 물고기" 색으로 바꾸는 함수 호출해줘요
        } else if (cookTime > overcookTime){
            // 타는 시간 < cookTime
            // material을 "탄 물고기" 색으로 바꾸는 함수 호출해줘요
        }
    }

    // cookable 제어 관련
    // Pan 콜라이더에 들어가면
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            cookable = true;
        }
    }
    // Pan 콜라이더에서 나가면
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            cookable = false;
        }
    }
    // 조리 완료 Material 변경 함수
    
    // 재료 불탐 Material 변경 함수
}