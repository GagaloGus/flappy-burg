using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        GameManager.instance.ChangeScene(sceneName);
    }

    public void ResetGame()
    {
        GameManager.instance.ResetGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame(bool pause)
    {
        GameManager.instance.gamePaused = pause;
    }
}
