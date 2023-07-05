using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 생선에 붙는 컴포넌트
public class Fish : MonoBehaviour
{
    //변수
    private GameManager _gameManager;
    public bool cookable;
    public bool isServing;
    public Material[] matbody = new Material[3];
    /**
     * cookState: 원하는 생선 종류
     * comment: 손님이 원하는 요리정도를 비교하기 위함
     * code 0: 날생선 상태
     * code 1: 잘익은 상태
     * code 2: 탄 상태
     */
    public int cookState = 0;
    
    public float cookTime = 0.0f;
    public float wellcookTime = 5f;
    public float overcookTime = 10f;

    public float servingTime = 0.0f;
    public float servingFinishTime = 0.5f;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cookState = 0;
        RawMat();
    }

    void Update()
    {
        // update안에서 if Pan 위면 Time.deltaTime을 조리시간에 +=
        if (cookable)
        {
            cookTime += Time.deltaTime;
            //SoundManager.instance.PlaySE("FishCooking");
        }

        // update안에서 if 배식대(서빙대) 위면 Time.deltaTime을 서빙시간에 +=
        if (isServing)
        {
            servingTime += Time.deltaTime;
        }

        // 조리된 총 시간 합이 생선 종류 경계 일때마다
        // 시간 조건에 따라서 Material을 바꾸는 함수를 호출
        if (cookTime < wellcookTime)
        {
            if (cookState != 0)
            {
                RawMat();
                cookState = 0;
            }
        }
        else if (cookTime >= wellcookTime && cookTime < overcookTime )
        {    
            // 잘익은 시간 < cookTime < 타는 시간
            // material을 "잘익은 물고기" 색으로 바꾸는 함수 호출해줘요
            if (cookState != 1)
            {
                CookedMat();
                cookState = 1;
            }
        } 
        else if (cookTime >= overcookTime)
        {
            // 타는 시간 < cookTime
            // material을 "탄 물고기" 색으로 바꾸는 함수 호출해줘요
            if (cookState != 2)
            {
                BurntMat();
                cookState = 2;
            }
        }

        if (servingTime > servingFinishTime) // 서빙 시간이 끝나면 
        {
            // 손님 주문 비교판정 함수 호출
            //정상호출 시
            if (_gameManager.ServingCustomer(cookState))
            {
                // 해당 오브젝트 비활성화
                gameObject.SetActive(false);
            }
            else
            {
                // 실패시 다시 서빙시간 기다림
                servingTime = 0f;
            }
        }
    }

    // cookable 제어 관련
    // Pan 콜라이더에 들어가면
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            cookable = true;
            SoundManager.instance.PlaySfx(SoundManager.Sfx.FishCooking);
        }
        else if (other.CompareTag("Servertable"))
        {
            isServing = true;
        }
    }
    // Pan 콜라이더에서 나가면
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            cookable = false;
        }
        else if (other.CompareTag("Servertable"))
        {
            isServing = false;
        }
    }
    // 조리 완료 Material 변경 함수
    public void RawMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[0];
    }
    // 조리 완료 Material 변경 함수
    public void CookedMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[1];
    }
    // 재료 불탐 Material 변경 함수
    public void BurntMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[2];
    }
}