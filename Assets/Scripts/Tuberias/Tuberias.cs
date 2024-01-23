using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuberias : MonoBehaviour
{
    float speed, lifeTime;
    float timer;
    Rigidbody rb;

    BoxCollider pointBCol;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;

        //Pone en true el trigger que suma puntos
        pointBCol = transform.Find("detectot").GetComponent<BoxCollider>();
        pointBCol.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Si nos morimos las tuberias paran
        if(!GameManager.instance.playerDied) { rb.velocity = Vector3.forward * speed * GameManager.instance.gameSpeed; }
        else { rb.velocity = Vector3.zero; }

        timer += Time.deltaTime;
        if(timer > lifeTime)
        {
            //Pone en true el trigger que suma puntos
            pointBCol.enabled = true;
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    public float tub_speed
    {
        set { speed = value;}
    }

    public float tub_life
    {
        set { lifeTime = value;}
    }
}
