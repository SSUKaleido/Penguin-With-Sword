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
    private GameManager _gameManager;

    private bool __FLAG__ = false;
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                if (__FLAG__ == false)
                {
                    _gameManager.StageClear();
                    __FLAG__ = true;
                    Time.timeScale = 0;
                }
            }
        }
    }
}
