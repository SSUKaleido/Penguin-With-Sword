using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Customer;
    public GameObject[] Customers;

    public GameObject PoolManager;

    private Transform CustomerWaypointsTransform;
    // Start is called before the first frame update
    void Start()
    {
        CustomerWaypointsTransform = GameObject.FindGameObjectWithTag("CustomerWaypoints").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnCustomer();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ServingCustomer();
        }
    }

    public void SpawnCustomer()
    {
        // GameObject customer = (GameObject)Instantiate(Customer, CustomerWaypointsTransform.position, CustomerWaypointsTransform.rotation);
        GameObject initCustomer;
        CustomerMovement initCustomerMovement;
        initCustomer = PoolManager.GetComponent<PoolManager>().Get(0);
        initCustomerMovement = initCustomer.GetComponent<CustomerMovement>();
        initCustomerMovement.customerStateCode = 0;
        initCustomerMovement.waypointEnterIndex = 0;
        initCustomerMovement.waypointExitIndex = 0;
    }

    public void ServingCustomer()
    {
        Customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (var _gameObject in Customers)
        {
            CustomerMovement _customerMovement;
            if (_gameObject.TryGetComponent<CustomerMovement>(out _customerMovement))
            {
                if (_customerMovement.GetCustomerStateCode() == 1)
                {
                    _customerMovement.SetSpeed(10);
                    _customerMovement.SetCustomerStateCode(2);
                }
            }
        }
    }
}
