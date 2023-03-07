using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject grabSlot;
    private bool canpickup;
    private GameObject ObjectIwantToPickUp;
    private bool hasItem;
    
    void Start()
    {
        canpickup = false;
        hasItem = false;
    }
    
    void Update()
    {
        if(canpickup == true)
        {
            if (Input.GetKeyDown("e"))
            {
                if (hasItem == true)
                {
                    Debug.Log("Has An Item. You Should drop your item with Key 'Q'");
                    return;
                }
                else
                {
                    ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    ObjectIwantToPickUp.transform.position = grabSlot.transform.position;
                    ObjectIwantToPickUp.transform.parent = grabSlot.transform;
                    hasItem = true;
                }
            }
        }
        if (Input.GetKeyDown("q"))
        {
            if (hasItem == true)
            {
                
            }
            ObjectIwantToPickUp.transform.parent = null;
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;
            hasItem = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("log"))
        {
            Debug.Log("OnTriggerEnter: Found log");
            canpickup = true;
            ObjectIwantToPickUp = other.gameObject;
            Debug.Log("You can pick up Item with Key"+"e");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: Can Pickup False");
        canpickup = false;
    }
}
