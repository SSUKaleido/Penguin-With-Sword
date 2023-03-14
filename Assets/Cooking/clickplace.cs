using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickplace : MonoBehaviour
{
    public Transform cloneObj;
    public int foodValue;
    
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
        cookflow.Platevalue[cookflow.PlateNum] += foodValue;
        
        Debug.Log(cookflow.Platevalue[cookflow.PlateNum] +"  "+ cookflow.Ordervalue[cookflow.PlateNum]);
        
    }
}
