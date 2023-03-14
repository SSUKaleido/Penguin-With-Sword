using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookflow : MonoBehaviour
{
    public static int [] Ordervalue = { 0, 1};
    public static int [] Platevalue = { 0, 0};
    public static float[] OrderTimer = { 60, 60, 60 };

    public static int PlateNum = 0;
    public static float PlateXpos = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            PlateNum += 1;
            PlateXpos += 2;

            if (PlateNum > 2)
            {
                PlateNum = 0;
                PlateXpos = 0;
            }
        }

        OrderTimer[0] -= Time.deltaTime;
        OrderTimer[1] -= Time.deltaTime;

        PlateSelector.transform.position = new Vector3(PlateXpos, 0, 0);

    }
}
