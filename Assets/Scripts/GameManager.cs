using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameObject player;

    public int points { get; private set; }
    public bool gamePaused;

    public List<int> highScore = new();
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
        player.transform.position = new(0, 0, -4);
        gamePaused = false;
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

    public void ChangeScene(string sceneName)
    {
        points = 0;
        gamePaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void ResetGame()
    {
        if (!highScore.Contains(points))
        {
            highScore.Add(points);
        }

        points = 0;
        ChangeScene("Game");
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }
}
