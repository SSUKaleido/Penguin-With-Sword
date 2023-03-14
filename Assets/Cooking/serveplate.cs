using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serveplate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (cookflow.Ordervalue[cookflow.PlateNum] == cookflow.Platevalue[cookflow.PlateNum])
        {
            Debug.Log("correct"+" "+cookflow.OrderTimer[cookflow.PlateNum]);
        }
    }
}
