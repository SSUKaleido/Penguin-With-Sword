using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class Grab : MonoBehaviour
{
    public GameObject grabSlot;
    [SerializeField] private bool hasItem;
    [SerializeField] private Transform pickedObject;
    [SerializeField] private Transform pickupObjectParent;
    [SerializeField] private Transform closestObject = null;

    [SerializeField]private Animator _playerAnimator;
    private PoolManager _poolManager;
    
    void Start()
    {
        _playerAnimator = transform.parent.GetComponentInChildren<Animator>();
        _poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
    }
    
    void Update()
    {
        //잡고 있을 떈 놓고 잡아야 하는 시스템
        if (Input.GetKeyDown("e"))
        {
            if (hasItem == true)//버리기, 놓기
            {
                if (!closestObject)
                {
                    DropPickedObjectObject();
                }
                else if (closestObject.CompareTag("Servertable")) // 서빙대
                {
                    DropPickedObjectObject(closestObject.position + new Vector3(0, 1, 0));
                }
                else if (closestObject.CompareTag("Kitchen")) // 조리대 
                {
                    DropPickedObjectObject(closestObject.position + new Vector3(0, 1, 0));
                }
            }
            else
            {
                if (!closestObject)
                {
                    //예외 처리
                }
                else if (closestObject.CompareTag("log") || closestObject.CompareTag("Grabable"))//근처에 있는 거 줍기
                {
                    GrabClosestObject(closestObject);
                }
                else if (closestObject.CompareTag("DiveTrigger"))//근처에 있는 오브젝트 풀업 하기
                {
                    _playerAnimator.SetTrigger("DiveTriggerOn");
                    GrabClosestObject(_poolManager.Get(1).transform);
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerExit: Can Pickup False");
        if (hasItem)
        {
            if (other.gameObject.CompareTag("Servertable")
                || other.gameObject.CompareTag("Kitchen"))
            {
                closestObject = null;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("log")
                || other.gameObject.CompareTag("Grabable")
                || other.gameObject.CompareTag("DiveTrigger"))
            {
                closestObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasItem)
        {
            if (other.gameObject.CompareTag("Servertable")
                || other.gameObject.CompareTag("Kitchen"))
            {
                SetClosestObject(other);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("log")
                || other.gameObject.CompareTag("Grabable")
                || other.gameObject.CompareTag("DiveTrigger"))
            {
                SetClosestObject(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 가장 가까운 물체가 나간 경우
        if (other.transform == closestObject)
        {
            closestObject = null;
        }
    }

    void SetClosestObject(Collider other)
    {
        // 가장 가까운 물체를 찾기 위해 거리 계산
        float distance = Vector3.Distance(transform.position, other.transform.position);

        // closestObject가 null이거나, 현재 물체가 더 가까운 경우
        if (closestObject == null || distance < Vector3.Distance(transform.position, closestObject.position))
        {
            closestObject = other.transform;
        }
    }

    void GrabClosestObject(Transform grabObject)
    {
        pickupObjectParent = grabObject.parent;
        grabObject.position = grabSlot.transform.position;
        grabObject.parent = grabSlot.transform;
        grabObject.GetComponent<Rigidbody>().isKinematic = true;
        grabObject.GetComponent<Collider>().isTrigger = true;
        grabObject.GetComponent<Collider>().enabled = false;
        pickedObject = grabObject;
        closestObject = null;
        hasItem = true;
        _playerAnimator.SetBool("IsWithObject", hasItem);
    }

    void DropPickedObjectObject(Vector3 dropPosition = default)
    {
        if (dropPosition != default)
        {
            pickedObject.position = dropPosition;
        }
        pickedObject.parent = pickupObjectParent;
        pickedObject.GetComponent<Collider>().isTrigger = false;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.GetComponent<Collider>().enabled = true;
        hasItem = false;
        _playerAnimator.SetBool("IsWithObject", hasItem);
    }
}
