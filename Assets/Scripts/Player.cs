using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float jumpHeight;
    public float maxXRotation;

    Rigidbody rb;
    Vector3 originalPos;

    [Header("Death")]
    public Vector3 deathLaunchDir;

    [Header("SFXs")]
    public AudioClip sfxBounce;
    public AudioClip sfxPoint;
    public AudioClip[] sfxDeath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GetComponent<CapsuleCollider>().enabled = true;

        //coje la posicion inicial al empezar el juego
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

        if(Input.GetKeyDown(KeyCode.Space))
        { Tap(); }

#elif UNITY_ANDROID

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            { Tap(); }
        }
#endif
        //Si no estamos muertos
        if (!GameManager.instance.playerDied) 
        {
            //Ajusta la posicion X y Z a la misma para que el player no se mueva en esos ejes
            transform.position = new(originalPos.x, transform.position.y, originalPos.z);
            Tilting();

            rb.velocity *= GameManager.instance.gameSpeed;
            GetComponent<ConstantForce>().force *= GameManager.instance.gameSpeed;
        }
    }

    void Tap()
    {
        //Si no estamos muertos, lanza al player hacia arriba
        if (!GameManager.instance.playerDied && GameManager.instance.gameSpeed != 0)
        {
            rb.velocity = Vector3.up * jumpHeight * GameManager.instance.gameSpeed;
            AudioPlayer.instance.PlaySFX(sfxBounce, 0.8f);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        //Si toca el trigger de puntos
        if (trigger.gameObject.CompareTag("tuboTrigger"))
        {
            trigger.gameObject.GetComponent<BoxCollider>().enabled = false;
            AudioPlayer.instance.PlaySFX(sfxPoint, 0.25f);
            GameManager.instance.AddPoints(1);
        }

        //Si toca el trigger de muerte
        if (trigger.gameObject.CompareTag("tuboTubo"))
        {
            Death();
        }

    }

    void Death()
    {
        //Lanza al player hacia atras y arriba
        rb.velocity = deathLaunchDir;

        //Rota
        StartCoroutine(RotateTowards(new(1, 0, 0), 8, 4));

        //Desactiva su collider
        GetComponent<CapsuleCollider>().enabled = false;

        //Reproduce un sfx de muerte aleatorio
        int rnd = Random.Range(0, sfxDeath.Length);
        AudioPlayer.instance.PlaySFX(sfxDeath[rnd]);
        
        GameManager.instance.Death();
    }

    void Tilting()
    {
        //Rota al player en el eje Y segun su velocidad en Y
        transform.rotation = Quaternion.Euler(CoolFunctions.MapValues(rb.velocity.y, -11, 11, -maxXRotation, maxXRotation), 180, 0);
    }

    IEnumerator RotateTowards(Vector3 rotateDirection, float turnSpeed, int duration)
    {
        //Rota hacia una direccion
        for (float i = 0; i < duration; i+= Time.deltaTime*2)
        {
            transform.rotation *= Quaternion.Euler(
                turnSpeed * rotateDirection.normalized.x,
                turnSpeed * rotateDirection.normalized.y,
                turnSpeed * rotateDirection.normalized.z);     
            
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }
}
