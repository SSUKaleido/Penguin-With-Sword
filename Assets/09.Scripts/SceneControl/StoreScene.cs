using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreScene : MonoBehaviour
{
    IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(1.5f);
    }
    
    public void UnloadStore()
    {
        StartCoroutine(ButtonDelay());
        var lastSceneIndex = SceneManager.sceneCount -1;
        var lastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
        SceneManager.UnloadSceneAsync(lastLoadedScene);
    }
}
