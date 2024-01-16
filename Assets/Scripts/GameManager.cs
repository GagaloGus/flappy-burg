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
    public int gameSpeed { get; private set; }
    public bool gamePaused;
    string sceneToChange = "";

    [SerializeField]
    int deathCount;
    int randomDeathCount;

    public List<int> highScore = new();

    [HideInInspector]
    public bool playerDied;
    bool disable;
    public string filename;
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
        if(SceneManager.GetActiveScene().name == "Game")
        {
            player = FindObjectOfType<Player>().gameObject;
            player.transform.position = new(0, 0, -4);
        }

        gameSpeed = 1;
        gamePaused = false;
        playerDied = false;

        randomDeathCount = UnityEngine.Random.Range(3, 6);

        try { LoadInStartup(); }
        catch { print("No JSON save file to load"); }
        
    }

    public void LoadInStartup()
    {
        highScore = JSONFileManager.instance.Load(filename);

        CoolFunctions.RemoveDuplicateValues(ref highScore); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }

        if (Input.GetKeyDown(KeyCode.G)) { Save(); }
    }

    public void Death()
    {
        playerDied = true;
        deathCount++;
        ResetGame();
    }

    public void ResetGame()
    {
        if (!highScore.Contains(points))
        {
            highScore.Add(points);
        }

        ChangeSceneTransition("Game");
    }

    public void ChangeSceneTransition(string sceneName)
    {
        gamePaused = false;
        gameSpeed = 0;
        FindObjectOfType<CanvasController>().FadeIn();

        StartCoroutine(WaitForTransition());
        sceneToChange = sceneName;
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(2);
        if (deathCount >= randomDeathCount && !disable)
        {
            deathCount = 0;
            randomDeathCount = UnityEngine.Random.Range(3, 6);
            FindObjectOfType<InterstitialAd>().ShowAd();
            gamePaused = true;
        }
        else
        {
            ChangeScene();
        }
    }

    public void ChangeScene(string sceneName)
    {
        sceneToChange = sceneName;
        ChangeScene();
    }

    public void ChangeScene()
    {
        bool temp = disable;
        SceneManager.LoadScene(sceneToChange);
        points = 0;
        disable = temp;
        gameSpeed = 1;
        gamePaused = false;
        playerDied = false;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void Disable()
    {
        disable = true;
    }

    void Save()
    {

        JSONFileManager.instance.Save(filename, highScore);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
