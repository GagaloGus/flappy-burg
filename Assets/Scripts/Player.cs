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

    [Header("SFXs")]
    public AudioClip sfxBounce;
    public AudioClip sfxPoint;
    public AudioClip sfxDeath;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (tap)
        {
            rb.velocity = new(0, jumpHeight, 0);
            AudioPlayer.instance.PlaySFX(sfxBounce);
            tap = false;
        }

        transform.position = new(originalPos.x, transform.position.y, originalPos.z);

        Tilting();
    }

    private void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag);
        if (collider.gameObject.CompareTag("tuboTrigger"))
        {
            GameManager.instance.AddPoints(1);
        }
    }

    void Tilting()
    {
        transform.rotation = Quaternion.Euler(CoolFunctions.MapValues(rb.velocity.y, -11, 11, -maxXRotation, maxXRotation), 180, 0);
    }

}
