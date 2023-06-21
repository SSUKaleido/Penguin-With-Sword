using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stageNumber = 0;
    public static int staticStageNumber = 0;
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
