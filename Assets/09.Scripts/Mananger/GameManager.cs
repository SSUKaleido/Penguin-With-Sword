using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject gameOverUI;
    public GameObject gameCompleteUI;
    public bool isDone = false;
    
    public GameOverScreen GameOverScreen;

    public Camera mainCamera;

    [SerializeField] private float alpha;
    //public GameObject Canvas;

    private float playTime;

    private int stageScore = 0;

    private StageManager stageManager;

    public GameObject clearTextUi;
    public GameObject scorePointerUi;
    
    public GameObject reduceHeartParticlePrefab;
    public Transform particleGroup;
    
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
        // scorePointerUiTextMeshPro = scorePointerUi.GetComponent<TextMeshProUGUI>();
        
        //메인 BGM 재생
        SoundManager.instance.PlayBgm(true);
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

        // if (isDone == true)
        // {
        //     GameOver();
        // }
        UpdatePoint();
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
                        PlayerPrefs.SetInt("servingSuccessCount",PlayerPrefs.GetInt("servingSuccessCount")+1);
                        AddStageScore();
                    }
                    else
                    {
                        //실패
                        Debug.Log("서빙 실패!!!");
                        ReduceHeart(_gameObject.transform.position);
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

    public void ReduceHeart(Vector3 position)
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
            GameObject particleObject = Instantiate(reduceHeartParticlePrefab, position + new Vector3(0,4,0), particleGroup.rotation, particleGroup);
            particleObject.transform.localScale = new Vector3(2,2,2);
            particleObject.transform.LookAt(mainCamera.transform);
            UpdateHeartStatus();
        }
    }

    public void UpdatePoint()
    {
        // scorePointerUiTextMeshPro.text = stageScore + "";
        scorePointerUi.GetComponent<TextMeshProUGUI>().text = stageScore + "";
    }

    public void GameOver()
    {
        Debug.Log("게임오버!!!");
        gameOverUI.SetActive(true);
        SaveStageScore();
    }
    
    public void StageClear()
    {
        Debug.Log("스테이지 클리어!!!");
        clearTextUi.GetComponent<TextMeshProUGUI>().text = "Stage Complete!!!\nYour Score is : " + stageScore;
        
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
        
        gameCompleteUI.SetActive(true);
        
        
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

    public void Restart()
    {
        // 레벨 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("StageSelectScene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
