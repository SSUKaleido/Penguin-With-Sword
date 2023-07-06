using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class Grab : MonoBehaviour
{
    public GameObject grabSlot;
    [SerializeField] private bool hasItem;
    [SerializeField] private Transform pickedObject;
    [SerializeField] private Transform pickupObjectParent;
    [SerializeField] private Transform closestObject = null;

    [SerializeField] private GameObject Model;
    private Animator _playerAnimator;
    
    [SerializeField] private GameObject PoolManagerObject;
    private PoolManager _poolManager;
    
    [SerializeField] private GameObject GameManagerObject;
    public GameManager gameManager;


    public GameObject smashParticlePrefab;
    public Transform particleGroup;

    [SerializeField] private GameObject InteractionButton;
    [SerializeField] private GameObject _player;
    [SerializeField] private bool isButtonPressing=false;
    //public int GetFishCount = 0;
    //public int GetCaptainSlap = 0;
    [SerializeField] private bool isPlayerDiving=false;
    
    void Start()
    {
        _playerAnimator = Model.GetComponent<Animator>();
        _poolManager = PoolManagerObject.GetComponent<PoolManager>();
        gameManager = GameManagerObject.GetComponent<GameManager>();
    }
    
    void Update()
    {
        //isPlayerDiving = _player.GetComponent<PlayerTouchMovement>().isDiving;
        isButtonPressing = _player.GetComponent<PlayerTouchMovement>().isInteractionButtonOn;
        //잡고 있을 떈 놓고 잡아야 하는 시스템
        if (isButtonPressing)
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
                else if (closestObject.CompareTag("DiveTrigger")) //다이빙해서 물고기 잡기
                {
                    //_playerAnimator.SetTrigger("isDiveReady");
                    //Player Jump and Up
                    GrabClosestObject(_poolManager.Get(1).transform);
                    SoundManager.instance.PlaySfx(SoundManager.Sfx.Penguin_diving);
                }
                else if (closestObject.CompareTag("Captain")) //선장 깨우기
                {
                    _playerAnimator.SetTrigger("Smash");
                    Captain captain =  closestObject.GetComponent<Captain>();
                    SoundManager.instance.PlaySfx(SoundManager.Sfx.penguin_smash);
                    
                    GameObject particleObject = Instantiate(smashParticlePrefab, grabSlot.transform.position, particleGroup.rotation, particleGroup);
                    particleObject.transform.localScale = new Vector3(2,2,2);
                    if (captain.isSleeping)
                    {
                        gameManager.AddStageScore();
                        captain.CaptainAwake();
                    }
                    
                }
                
            }
            _player.GetComponent<PlayerTouchMovement>().isInteractionButtonOn = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("OnTriggerExit: Can Pickup False");
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
                || other.gameObject.CompareTag("DiveTrigger")
                || other.gameObject.CompareTag("Captain"))
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
                || other.gameObject.CompareTag("DiveTrigger")
                || other.gameObject.CompareTag("Captain"))
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
        _playerAnimator.SetBool("isHolding",hasItem);
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
        _playerAnimator.SetBool("isHolding",hasItem);
    }
}
