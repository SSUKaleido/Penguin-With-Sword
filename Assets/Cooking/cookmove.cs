using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookmove : MonoBehaviour
{
    private int FoodValue = 0;

    private MeshRenderer Meatmat;

    private string stillcooking = "y";
    
    // Start is called before the first frame update
    void Start()
    {
        Meatmat = GetComponent<MeshRenderer>();
        StartCoroutine(Cooktimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GetComponent<Transform>().position = new Vector3(0, .66f, 0);
        cookflow.Platevalue[cookflow.PlateNum] += FoodValue;
        stillcooking = "n";
    }

    IEnumerator Cooktimer()
    {
        yield return new WaitForSeconds(10);
        FoodValue = 1;
        if (stillcooking == "y")
        {
            Meatmat.material.color = new Color(.3f, .3f, .3f);
        }
    }
}
