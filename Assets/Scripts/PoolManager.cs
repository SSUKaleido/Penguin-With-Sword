using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Var for Prefab
    public GameObject[] prefabs;
    
    // Pooling List
    private List<GameObject>[] pools;

    private Transform CustomerWaypointsTransform;
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
        
        CustomerWaypointsTransform = GameObject.FindGameObjectWithTag("CustomerWaypointsEnter").GetComponent<Transform>();
    }

    /**
     * index에 해당하는 Prefabs을 Pooling합니다.
     * index 0: customer penguin
     */
    public GameObject Get(int index)
    {
        GameObject selectedGameObject = null;
        
        // 선택한 풀의 비활성화 게임오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            //발견 시 selectedGameObject에 할당
            if (!item.activeSelf)
            {
                selectedGameObject = item;
                selectedGameObject.SetActive(true);
                break;
            }    
        }
        // 접근 실패 시 
        if (!selectedGameObject)
        {
            //새롭게 생성하고 selectedGameObject에 할당
            selectedGameObject = Instantiate(prefabs[index], transform);
            pools[index].Add(selectedGameObject);
        }

        //각 프리팹 초기화
        switch (index)
        {
            case 0:
                CustomerMovement initCustomerMovement;
                selectedGameObject.transform.position = CustomerWaypointsTransform.position;
                initCustomerMovement = selectedGameObject.GetComponent<CustomerMovement>();
                initCustomerMovement.customerStateCode = 0;
                initCustomerMovement.waypointEnterIndex = 0;
                initCustomerMovement.waypointExitIndex = 0;
                initCustomerMovement.SetActiveMenuPop(true);
                initCustomerMovement.SetRandomCustomerWantedCookState();
                break;
            // case 1: break;
        }

        return selectedGameObject;
    }
}
