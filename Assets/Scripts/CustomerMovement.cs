using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotSteed = 30f;
    private Vector3 dir = Vector3.zero;

    private CustomerWaypoint _customerWaypoint;

    private int _wayPointIndex;

    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _customerWaypoint = GameObject.FindGameObjectWithTag("CustomerWaypoints").GetComponent<CustomerWaypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = _transform.position;
        // _wayPointIndex 번째 웨이포인트로 이동 
        _transform.position = Vector3.MoveTowards(currentPosition, _customerWaypoint.waypoints[_wayPointIndex].position,
            speed * Time.deltaTime);
        
        // 다음 웨이포인트를 향한 방향으로 회전
        dir = _customerWaypoint.waypoints[_wayPointIndex].position - currentPosition;
        dir.y = 0f;
        dir.Normalize();
        _transform.forward = Vector3.Lerp(_transform.forward, dir, rotSteed * Time.deltaTime);

        // _wayPointIndex 번째 웨이포인트에 근접했을 경우
        if (Vector3.Distance(currentPosition, _customerWaypoint.waypoints[_wayPointIndex].position) < 0.1f)
        {
            // 다음 웨이포인트가 있으면 인덱스를 1 증가시켜 다음 웨이포인트로 향하게 한다.
            if (_wayPointIndex < _customerWaypoint.waypoints.Length-1)
            {
                _wayPointIndex++;    
            }
            // 마지막 웨이포인트에 도착한 경우 음식을 주문한다
            // 현재는 속도를 0으로 바꿈
            else
            {
                speed = 0f;
            }
        }
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
}
