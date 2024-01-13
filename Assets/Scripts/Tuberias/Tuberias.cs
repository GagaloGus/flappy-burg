using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuberias : MonoBehaviour
{
    float speed, lifeTime;
    float timer;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.forward * speed;

        timer += Time.deltaTime;
        if(timer > lifeTime)
        {
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
