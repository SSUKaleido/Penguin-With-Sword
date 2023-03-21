using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotSteed = 30f;
    private Vector3 dir = Vector3.zero;

    private CustomerWaypoint _customerWaypointEnter;
    private CustomerWaypoint _customerWaypointExit;

    public int waypointEnterIndex = 0;
    public int waypointExitIndex = 0;

    public int customerStateCode = 0;
    /**
     * customerStateCode: 손님의 상태
     * comment: 현재 손님이 음식을 받기전인지 받은 후인지 판단
     * 0: 서빙대 도착 전
     * 1: 서빙대 도착 후 음식을 받기 전
     * 2: 음식을 받은 후
     */

    public int customerWantedCookState = 1;
    /**
     * customerWantedCookState: 원하는 생선 종류
     * comment: 손님이 원하는 요리정도
     * code 0: 날생선 상태
     * code 1: 잘익은 상태
     * code 2: 탄 상태
     */
    
    private Transform _transform;

    private Animator _customerAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _customerWaypointEnter = GameObject.FindGameObjectWithTag("CustomerWaypointsEnter").GetComponent<CustomerWaypoint>();
        _customerWaypointExit = GameObject.FindGameObjectWithTag("CustomerWaypointsExit").GetComponent<CustomerWaypoint>();
        _customerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = _transform.position;
        
        //GetNextWaypointVector3로 다음 웨이포인트의 값을 불러옴
        Vector3 targetPointVector3 = GetNextWaypointVector3();
        
        // _wayPointIndex 번째 웨이포인트로 이동 
        _transform.position = Vector3.MoveTowards(currentPosition, targetPointVector3,
            speed * Time.deltaTime);
        
        // 다음 웨이포인트를 향한 방향으로 회전
        dir = targetPointVector3 - currentPosition;
        dir.y = 0f;
        dir.Normalize();
        _transform.forward = Vector3.Lerp(_transform.forward, dir, rotSteed * Time.deltaTime);

        // _wayPointIndex 번째 웨이포인트에 근접했을 경우
        if (Vector3.Distance(currentPosition, targetPointVector3) < 0.1f)
        {
            //서빙대 도착 전
            if (customerStateCode == 0)
            {
                // 다음 웨이포인트가 있으면 인덱스를 1 증가시켜 다음 웨이포인트로 향하게 한다.
                if (waypointEnterIndex < _customerWaypointEnter.waypoints.Length-1)
                {
                    waypointEnterIndex++;    
                }
                // 마지막 웨이포인트에 도착한 경우 음식을 주문한다
                else
                {
                    customerStateCode = 1;
                    speed = 0f;
                }
            }
            //음식을 받은 후
            else if (customerStateCode == 2)
            {
                //말풍선 제거 로직 필요
                SetActiveMenuPop(false);
                
                // 다음 웨이포인트가 있으면 인덱스를 1 증가시켜 다음 웨이포인트로 향하게 한다.
                if (waypointExitIndex < _customerWaypointExit.waypoints.Length-1)
                {
                    waypointExitIndex++;   
                }
                // 마지막 웨이포인트에 도착한 경우 제거
                else
                {
                    // customerStateCode = 1;
                    // speed = 0f;
                    this.gameObject.SetActive(false);
                }
            }
        }
        
        //애니메이션 컨트롤
        _customerAnimator.SetFloat("Velocity", speed * (customerStateCode == 0 || customerStateCode == 2 ? 1 : 0));
    }

    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float paramSpeed)
    {
        speed = paramSpeed;
        return;
    }
    public int GetCustomerStateCode()
    {
        return customerStateCode;
    }
    public void SetCustomerStateCode(int paramCustomerStateCode)
    {
        customerStateCode = paramCustomerStateCode;
        return;
    }

    private Vector3 GetNextWaypointVector3()
    {
        Vector3 _nextWaypointVector3 = Vector3.zero;
        if (customerStateCode == 2)
        {
            _nextWaypointVector3 = _customerWaypointExit.waypoints[waypointExitIndex].position;
        }
        else
        {
            _nextWaypointVector3 = _customerWaypointEnter.waypoints[waypointEnterIndex].position;
        }

        return _nextWaypointVector3;
    }
    
    public void SetActiveMenuPop(bool isActive)
    {
        transform.GetChild(3).gameObject.SetActive(isActive);
    }

    public int SetCustomerWantedCookState(int paramCode)
    {
        customerWantedCookState = paramCode;
        for (int i=0;i<3;i++)
        {
            transform.Find("CustomerMenuCanvas/fish"+i).gameObject.SetActive(i == paramCode?true:false);
        }
        return customerWantedCookState;
    }
    
    public int SetRandomCustomerWantedCookState()
    {
        int randomWantedCookState = 1;
        int randomVal = Random.Range(0, 10);

        if (randomVal < 1) // 10%확률로 날생선
        {
            randomWantedCookState = 0;
        } 
        else if (randomVal < 3) // 20%확률로 탄생선
        {
            randomWantedCookState = 2;
        }
        else // 70%확률로 잘익은생선
        {
            randomWantedCookState = 1;
        }
        
        return SetCustomerWantedCookState(randomWantedCookState);
    }
}
