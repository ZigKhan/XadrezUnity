using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("chessgame");
        /*AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BrasilHerois");
        asyncOperation.allowSceneActivation = false;
        ExecuteAfterTime(1, asyncOperation);*/
        
    }
    public void PlayGameBrasil()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        SceneManager.LoadScene("BrasilHerois");
        /*AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BrasilHerois");
        asyncOperation.allowSceneActivation = false;
        ExecuteAfterTime(1, asyncOperation);*/
    }
    public void EndGame()
    {
        Application.Quit();
    }
    
    IEnumerator ExecuteAfterTime(float time, AsyncOperation asyncOperation)
    {
        yield return new WaitForSeconds(time);
        asyncOperation.allowSceneActivation = true;

    }
}

