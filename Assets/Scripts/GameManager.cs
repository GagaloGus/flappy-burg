using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int points;
    GameObject player;

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

        player = FindObjectOfType<Player>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetGame()
    {
        points = 0;
        player.transform.position = new(0, 0, -4);
        SceneManager.LoadScene(0);
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }


    public int gm_points
    {
        get { return points; }
    }
}
