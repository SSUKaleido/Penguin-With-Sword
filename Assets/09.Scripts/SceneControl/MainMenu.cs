using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //LoadingSceneController.LoadScene("mainScene");
        //LoadingSceneController.Instance.LoadScene("mainScene");
        SceneManager.LoadScene("mainScene");
    }

    public void QuickGame()
    {
        Application.Quit();
    }

}