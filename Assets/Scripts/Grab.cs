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
    [SerializeField]private GameObject ObjectIwantToPickUp;
    private bool hasItem;
    private Transform pickupObjectParent;
    public float lerpSpeed;
    private Vector3 GrabVector;
    private GameObject ObjectIPickedUp;

    private Animator _playerAnimator;
    void Start()
    {
        _playerAnimator = pickupObjectParent.GetComponentInChildren<Animator>();
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
                }
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("log"))
        {
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
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: Can Pickup False");
        canpickup = false;
    }                           
}
