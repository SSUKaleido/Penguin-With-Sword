using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class Grab : MonoBehaviour
{
    public GameObject grabSlot;
    private bool canpickup;
    private bool canobjectpool;
    [SerializeField] private GameObject ObjectIwantToPickUp;
    [SerializeField] private int IndexIwantToObjectPool;
    private bool hasItem;
    private Transform pickupObjectParent;
    private Vector3 GrabVector;
    private GameObject ObjectIPickedUp;

    [SerializeField]private Animator _playerAnimator;
    private PoolManager _poolManager;
    
    private List<Transform> objectsInRange = new List<Transform>();
    void Start()
    {
        _playerAnimator = transform.parent.GetComponentInChildren<Animator>();
        _poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        canpickup = false;
        hasItem = false;
    }
    
    void Update()
    {
        //잡고 있을 떈 놓고 잡아야 하는 시스템
        if (Input.GetKeyDown("e"))
        {
            if (hasItem == true)//버리기
            {
                ObjectIwantToPickUp.transform.parent = pickupObjectParent;
                ObjectIwantToPickUp.GetComponent<Collider>().isTrigger = false;
                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;
                hasItem = false;
                _playerAnimator.SetBool("IsWithObject", hasItem);
            }
            else
            {
                if (canpickup == true)//근처에 있는 거 줍기
                {
                    pickupObjectParent = ObjectIwantToPickUp.transform.parent;
                    ObjectIwantToPickUp.transform.position = Vector3.Slerp(ObjectIwantToPickUp.transform.position,grabSlot.transform.position,0.9f);
                    ObjectIwantToPickUp.transform.parent = grabSlot.transform;
                    ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    ObjectIwantToPickUp.GetComponent<Collider>().isTrigger = true;
                    hasItem = true;
                    _playerAnimator.SetBool("IsWithObject", hasItem);
                }
                else if (canobjectpool == true)//근처에 있는 오브젝트 풀업 하기
                {
                    ObjectIwantToPickUp = _poolManager.Get(IndexIwantToObjectPool);
                    pickupObjectParent = ObjectIwantToPickUp.transform.parent;
                    // ObjectIwantToPickUp.transform.position = Vector3.Slerp(ObjectIwantToPickUp.transform.position,grabSlot.transform.position,0.9f);
                    ObjectIwantToPickUp.transform.position = grabSlot.transform.position;
                    ObjectIwantToPickUp.transform.parent = grabSlot.transform;
                    ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    ObjectIwantToPickUp.GetComponent<Collider>().isTrigger = true;
                    hasItem = true;
                    _playerAnimator.SetBool("IsWithObject", hasItem);
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("log") || other.gameObject.CompareTag("Grabable"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            
            Debug.Log("OnTriggerEnter: Found log");
            canpickup = true;
            if (hasItem == false)
            {
                ObjectIwantToPickUp = other.gameObject;
                Debug.Log("You can pick up Item with Key"+"e");
            }
            else
            {
                Debug.Log("Has An Item. You Should drop your item with Key 'Q'");
            }
        }

        if (other.gameObject.CompareTag("DiveTrigger"))
        {
            canobjectpool = true;
            if (hasItem == false)
            {
                IndexIwantToObjectPool = 1;
                Debug.Log("You can pick up Item with Key"+"e");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: Can Pickup False");
        if(other.gameObject.CompareTag("log") || other.gameObject.CompareTag("Grabable"))
        {
            canpickup = false;
        }

        if (other.gameObject.CompareTag("DiveTrigger"))
        {
            canobjectpool = false;
        }
    }                           
}
