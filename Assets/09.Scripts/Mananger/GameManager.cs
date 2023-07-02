using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHeart = 3;
    private float currTime;
    [SerializeField]
    private int curentHeart;
    [FormerlySerializedAs("txt_HpArray")] [SerializeField] private Transform[] hpArray;
    
    public GameObject[] Customers;

    public GameObject PoolManager;

    public GameObject QuitCanvas;
    public bool isDone=false;

    public GameOverScreen GameOverScreen;

    [SerializeField] private float alpha;
    //public GameObject Canvas;

    private float playTime;

    private int stageScore = 0;

    private StageManager stageManager;
    
    // Start is called before the first frame update
    void Start()
    {
        curentHeart = maxHeart;
        UpdateHeartStatus();
        //alpha = QuitCanvas.GetComponent<CanvasGroup>().alpha;

        playTime = PlayerPrefs.GetFloat("playTime");
        Debug.Log("gameOverTimes : " + (PlayerPrefs.GetInt("gameOverTimes")));
        Debug.Log("playTime : " + playTime);

        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
        currTime += Time.deltaTime;
        if (currTime > 5)
        {
            SpawnCustomer();
            currTime = 0;
        }

        if (isDone == true)
        {
            QuitCanvas.SetActive(true);
        }
    }

    public void SpawnCustomer()
    {
        PoolManager.GetComponent<PoolManager>().Get(0);
    }

    public bool ServingCustomer(int cookState)
    {
        Customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (var _gameObject in Customers)
        {
            CustomerMovement _customerMovement;
            if (_gameObject.TryGetComponent<CustomerMovement>(out _customerMovement))
            {
                if (_customerMovement.GetCustomerStateCode() == 1)
                {
                    // 주문과 서빙 비교 로직
                    if (cookState == _customerMovement.customerWantedCookState)
                    {
                        //성공
                        Debug.Log("서빙 성공!!!");
                        PlayerPrefs.SetInt("survingSuccessCount",PlayerPrefs.GetInt("survingSuccessCount")+1);
                        AddStageScore();
                    }
                    else
                    {
                        //실패
                        Debug.Log("서빙 실패!!!");
                        ReduceHeart();
                    }
                    
                    _customerMovement.SetSpeed(10);
                    _customerMovement.SetCustomerStateCode(2);
                    return true;
                }
            }
        }
        return false;
    }


    void UpdateHeartStatus()
    {
        for (int i=0; i < hpArray.Length; i++)
        {
            if (i<curentHeart)
            {
                hpArray[i].gameObject.SetActive(true);
            }
            else
            {
                hpArray[i].gameObject.SetActive(false);
            }
        }
    }

    public void ReduceHeart()
    {
        curentHeart -= 1;
        if (curentHeart <= 0)
        {
            GameOver();
            PlayerPrefs.SetInt("gameOverTimes",PlayerPrefs.GetInt("gameOverTimes")+1);
            PlayerPrefs.SetFloat("playTime", playTime);
            PlayerPrefs.SetInt("fishGenTime", PoolManager.GetComponent<PoolManager>().GetFishGenTime());
            PlayerPrefs.SetInt("customerGenTime", PoolManager.GetComponent<PoolManager>().GetCustomerGenTime());
            PlayerPrefs.Save();
        }
        else
        {
            UpdateHeartStatus();
        }
    }

    public void GameOver()
    {
        Debug.Log("게임오버!!!");
        GameOverScreen.Setup();
        // TODO: 게임오버 로직 구현
        isDone = true;
        
        //GameObject A = Resources.Load("Image") as GameObject;
        //GameObject B = Instantiate(A);
        //B.transform.parent = Canvas.transform;
        //QuitCanvas.GetComponent<CanvasGroup>().alpha = 1;
        //alpha = 1;
        
        SaveStageScore();
    }
    
    public void StageClear()
    {
        Debug.Log("스테이지 클리어!!!");
        
        // TODO: 스테이지 클리어 화면으로 교체필요
        // GameOverScreen.Setup();
        
        // TODO: 스테이지 클리어 로직 구현
        // isDone = true;
        if (stageManager)
        {
            stageManager.SaveStageClearData();
            Destroy(GameObject.Find("StageManager"));
        }
        else
        {
            Debug.Log("stageManager is null");
        }
        // SaveStageScore() 사용 필요
        SaveStageScore();
        

        //TODO: (임시 스테이지 선택화면으로 이동) 삭제필요
        SceneManager.LoadScene("StageSelectScene");
    }

    public void AddStageScore(int gainScore = 10)
    {
        stageScore += gainScore;
    }

    /**
     * 게임 종료 화면에서 점수(재화) 저장
     */
    public void SaveStageScore()
    {
        PlayerPrefs.SetInt("userScore", PlayerPrefs.GetInt("userScore") + stageScore);
        PlayerPrefs.Save();
        
        Debug.Log("스테이지 점수 : " + stageScore);
        Debug.Log("유저가 보유한 포인트 : " + PlayerPrefs.GetInt("userScore"));
        
        stageScore = 0;
    }
}
