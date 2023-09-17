using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    public StageManager stageManager;
    public void GoToMainMenu() {
        SceneManager.LoadScene("Start");
    }
    
    public void GoToIndexStage(int _index) {
        // 인덱스 스테이지로 이동
        // 해당 스테이지의 난이도(미션 주기)로 게임 난이도 조정
        // SceneManager.LoadScene("Stage Scene " + _index);
        StageManager.staticStageNumber = _index;
        stageManager.stageNumber = _index;
        SceneManager.LoadScene("mainScene");
    }
    
    public void GoToShop() {
        // 상점 화면으로 이동
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("StoreScene"));
        SceneManager.LoadScene("StoreScene",LoadSceneMode.Additive);
    }
}
