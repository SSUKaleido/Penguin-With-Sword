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
    public Material[] matbody = new Material[3];
    public Material[] matfin = new Material[3];
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

    private void Start()
    {
        cookState = 0;
        RawMat();
    }

    void Update()
    {
        // update안에서 if Pan 위면 Time.deltaTime을 조리시간에 +=
        if (cookable)
        {
            cookTime += Time.deltaTime;
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
    public void RawMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[0];
        GetComponentsInChildren<MeshRenderer>()[1].material = matfin[0];
        GetComponentsInChildren<MeshRenderer>()[2].material = matfin[0];
        GetComponentsInChildren<MeshRenderer>()[3].material = matfin[0];
    }
    // 조리 완료 Material 변경 함수
    public void CookedMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[1];
        GetComponentsInChildren<MeshRenderer>()[1].material = matfin[1];
        GetComponentsInChildren<MeshRenderer>()[2].material = matfin[1];
        GetComponentsInChildren<MeshRenderer>()[3].material = matfin[1];
    }
    // 재료 불탐 Material 변경 함수
    public void BurntMat()
    {
        GetComponentsInChildren<MeshRenderer>()[0].material = matbody[2];
        GetComponentsInChildren<MeshRenderer>()[1].material = matfin[2];
        GetComponentsInChildren<MeshRenderer>()[2].material = matfin[2];
        GetComponentsInChildren<MeshRenderer>()[3].material = matfin[2];
    }
}