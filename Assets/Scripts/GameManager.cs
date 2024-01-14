using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player;

    public int points { get; private set; }
    public bool gamePaused;

    public List<int> highScore = new();

    [HideInInspector]
    public bool playerDied;

    public float transitionDuration;
    TransicionNegro transicion;
    void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro GameManager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro GameManager lo destruye.
        }

    }

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        transicion = FindObjectOfType<TransicionNegro>();
        transicion.StartFadeOut(transitionDuration);

        player.transform.position = new(0, 0, -4);
        gamePaused = false;
        playerDied = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void ResetGame()
    {
        if (!highScore.Contains(points))
        {
            highScore.Add(points);
        }

        points = 0;
        ChangeSceneTransition("Game");
    }

    public void ChangeSceneTransition(string sceneName)
    {
        transicion.StartFadeIn(transitionDuration);
        StartCoroutine(WaitChangeScene(sceneName, transitionDuration));
    }
    IEnumerator WaitChangeScene(string sceneName, float duration)
    {
        yield return new WaitForSeconds(duration);
        ChangeScene(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
        points = 0;
        SceneManager.LoadScene(sceneName);
        gamePaused = false;
        playerDied = false;
    }


    public void Death()
    {
        playerDied = true;
        ResetGame();
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

}
