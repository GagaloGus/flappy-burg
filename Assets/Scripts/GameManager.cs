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

    [SerializeField] int deathCount;
    [SerializeField] Vector2Int deathCountMinMax;
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
        //coje al player y pone su posicion en la de inicio si estamos en el juego
        if(SceneManager.GetActiveScene().name == "Game")
        {
            player = FindObjectOfType<Player>().gameObject;
            player.transform.position = new(0, 0, -4);
        }

        gameSpeed = 1;
        gamePaused = false;
        playerDied = false;

        //randomiza la cantidad de veces que nos tenemos que morir para que salga un ad
        randomDeathCount = UnityEngine.Random.Range(deathCountMinMax.x, deathCountMinMax.y);

        try { LoadInStartup(); }
        catch { print("No JSON save file to load"); }    
    }

    public void LoadInStartup()
    {
        highScore = JSONFileManager.Load(filename);

        CoolFunctions.RemoveDuplicateValues(ref highScore); 
    }

    private void Update()
    {
        if (gamePaused) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void Death()
    {
        playerDied = true;
        //añade 1 al contador de muertes
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
        sceneToChange = sceneName;
        
        FindObjectOfType<CanvasController>().FadeIn();
        //Espera 2 segundos (lo que dura la transicion) para cambiar de escena
        Invoke(nameof(AdCheck), 2);
    }


    void AdCheck()
    {
        if (deathCount >= randomDeathCount && !disable)
        {
            deathCount = 0;
            randomDeathCount = UnityEngine.Random.Range(deathCountMinMax.x, deathCountMinMax.y);
            FindObjectOfType<AdManager>().ShowAd();
            gamePaused = true;
        }
        else
        {
            ChangeScene();
        }
    }

    //hacemos una sobrecarga del mismo void que admita el nombre de la escena, por si acaso
    public void ChangeScene(string sceneName)
    {
        sceneToChange = sceneName;
        ChangeScene();
    }

    //Ajusta todos los parametros para que se cargue la escena correctamente
    public void ChangeScene()
    {
        bool temp = disable;
        SceneManager.LoadScene(sceneToChange);
        points = 0;
        disable = temp;
        gameSpeed = 1;
        gamePaused = false;
        playerDied = false;

        //Si cambiamos la escena a game y el audioplayer no esta reproduciendo nada, reproduce la cancioncita del juego
        if(sceneToChange == "Game" && !AudioPlayer.instance.musicSource.isPlaying)
        {
            AudioPlayer.instance.PlayMusic("juego", 1f);
        }
        //Si cambiamos al menu paramos todos los sonidos
        else if(sceneToChange == "Menu")
        {
            AudioPlayer.instance.StopAllSounds();
        }
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void Disable()
    {
        disable = true;
    }

    public void Save()
    {

        JSONFileManager.Save(filename, highScore);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
