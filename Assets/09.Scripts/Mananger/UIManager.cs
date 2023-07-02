using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float maxTime = 60f;
    private float timeLeft;
    private Image timerBar;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            {
                Time.timeScale = 0;
            }
        }
        
        // 여기에 점수 업데이트 만들어놓기
    }
}
