using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubo : MonoBehaviour
{
    MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if(Input.GetKeyDown(KeyCode.Space))
        {
            renderer.material.color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
        }
#elif UNITY_ANDROID
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                renderer.material.color = Color.yellow;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                renderer.material.color = Color.white;
            }
        }
#endif


    }
}
