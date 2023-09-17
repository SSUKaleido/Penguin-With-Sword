using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
