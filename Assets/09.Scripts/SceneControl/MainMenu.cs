using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(1.5f);
    }
    public void PlayGame()
    {
        //LoadingSceneController.LoadScene("mainScene");
        //LoadingSceneController.Instance.LoadScene("mainScene");
        StartCoroutine(ButtonDelay());
        SceneManager.LoadScene("mainScene");
    }

    public void QuickGame()
    {
        Application.Quit();
    }
}