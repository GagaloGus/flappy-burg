using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public TransicionNegro transicion;
    // Start is called before the first frame update
    void Start()
    {
        transicion.gameObject.SetActive(true);
        transicion.StartFadeOut();
    }

    public void FadeIn()
    {
        transicion.gameObject.SetActive(true);
        transicion.StartFadeIn();
    }

}
