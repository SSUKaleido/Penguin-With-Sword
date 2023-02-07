using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Customer;

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
    }

    public void SpawnCustomer()
    {
        GameObject customer = (GameObject)Instantiate(Customer, CustomerWaypointsTransform.position, CustomerWaypointsTransform.rotation);
    }
}
