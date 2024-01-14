using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool tap = false;
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

        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

        if(Input.GetKeyDown(KeyCode.Space))
        { tap = true; }

#elif UNITY_ANDROID

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            { tap = true; }
        }
#endif

        if (!GameManager.instance.playerDied) 
        {
            if (tap)
            {
                rb.velocity = new(0, jumpHeight, 0);
                AudioPlayer.instance.PlaySFX(sfxBounce);
                tap = false;
            }

            transform.position = new(originalPos.x, transform.position.y, originalPos.z);
            Tilting();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Death();
        }

    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("tuboTrigger"))
        {
            AudioPlayer.instance.PlaySFX(sfxPoint);
            GameManager.instance.AddPoints(1);
        }

        if (trigger.gameObject.CompareTag("tuboTubo"))
        {
            Death();
        }

    }

    void Death()
    {
        rb.velocity = deathLaunchDir;
        StartCoroutine(RotateTowards(new(1, 0, 0), 8, 4));
        GetComponent<CapsuleCollider>().enabled = false;

        int rnd = Random.Range(0, sfxDeath.Length);
        AudioPlayer.instance.PlaySFX(sfxDeath[rnd]);
        
        GameManager.instance.Death();
    }

    void Tilting()
    {
        transform.rotation = Quaternion.Euler(CoolFunctions.MapValues(rb.velocity.y, -11, 11, -maxXRotation, maxXRotation), 180, 0);
    }

    IEnumerator RotateTowards(Vector3 rotateDirection, float turnSpeed, int duration)
    {
        Vector3 originalRot = transform.rotation.eulerAngles;
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
