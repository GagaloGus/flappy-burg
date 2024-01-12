using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool tap = false;
    public float jumpHeight, maxXRotation;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            tap = false;
        }

        Tilting();
    }

    void Tilting()
    {

    }

}
