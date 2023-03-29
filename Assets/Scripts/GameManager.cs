using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHeart = 3;
    private float currTime;
    [SerializeField]
    private int curentHeart;
    [FormerlySerializedAs("txt_HpArray")] [SerializeField] private Transform[] hpArray;
    
    public GameObject[] Customers;

    public GameObject PoolManager;

    public GameObject QuitCanvas;
    public bool isDone=false;

    [SerializeField] private float alpha;
    //public GameObject Canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        curentHeart = maxHeart;
        UpdateHeartStatus();
        //alpha = QuitCanvas.GetComponent<CanvasGroup>().alpha;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > 5)
        {
            SpawnCustomer();
            currTime = 0;
        }

        if (isDone == true)
        {
            QuitCanvas.SetActive(true);
        }
    }

    public void SpawnCustomer()
    {
        PoolManager.GetComponent<PoolManager>().Get(0);
    }

    public bool ServingCustomer(int cookState)
    {
        Customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (var _gameObject in Customers)
        {
            CustomerMovement _customerMovement;
            if (_gameObject.TryGetComponent<CustomerMovement>(out _customerMovement))
            {
                if (_customerMovement.GetCustomerStateCode() == 1)
                {
                    // 주문과 서빙 비교 로직
                    if (cookState == _customerMovement.customerWantedCookState)
                    {
                        //성공
                        Debug.Log("서빙 성공!!!");
                    }
                    else
                    {
                        //실패
                        Debug.Log("서빙 실패!!!");
                        ReduceHeart();
                    }
                    
                    _customerMovement.SetSpeed(10);
                    _customerMovement.SetCustomerStateCode(2);

                    return true;
                }
            }
        }
        return false;
    }

    void UpdateHeartStatus()
    {
        for (int i=0; i < hpArray.Length; i++)
        {
            if (i<curentHeart)
            {
                hpArray[i].gameObject.SetActive(true);
            }
            else
            {
                hpArray[i].gameObject.SetActive(false);
            }
        }
    }

    public void ReduceHeart()
    {
        curentHeart -= 1;
        if (curentHeart <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateHeartStatus();
        }
    }

    public void GameOver()
    {
        Debug.Log("게임오버!!!");

        // TODO: 게임오버 로직 구현
        isDone = true;
        
        //GameObject A = Resources.Load("Image") as GameObject;
        //GameObject B = Instantiate(A);
        //B.transform.parent = Canvas.transform;
        //QuitCanvas.GetComponent<CanvasGroup>().alpha = 1;
        //alpha = 1;
    }
}
