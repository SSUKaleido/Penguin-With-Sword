using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int stageNumber = 0;
    public static int staticStageNumber = 0;

    public GameObject[] stageButtonList;

    private int bestClearedStageIndex;
    private int bestClearedStageNumber;
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        LoadStageClearData();
    }

    public void SaveStageClearData()
    {
        PlayerPrefs.SetInt("bestClearedStageIndex"
            , PlayerPrefs.GetInt("bestClearedStageIndex", -1) > stageNumber-1 ? PlayerPrefs.GetInt("bestClearedStageIndex", -1):stageNumber-1
            );
        PlayerPrefs.SetInt("bestClearedStageNumber"
            , PlayerPrefs.GetInt("bestClearedStageNumber", 0) > stageNumber ? PlayerPrefs.GetInt("bestClearedStageNumber", 0):stageNumber
            );
    }

    public void LoadStageClearData()
    {
        bestClearedStageIndex = PlayerPrefs.GetInt("bestClearedStageIndex", -1);
        bestClearedStageNumber = PlayerPrefs.GetInt("bestClearedStageNumber", 0);

        for (int idx=0; idx<stageButtonList.Length; idx++)
        {   
            stageButtonList[idx].GetComponent<Button>().interactable = (idx <= bestClearedStageIndex + 1);
        }
    }
}