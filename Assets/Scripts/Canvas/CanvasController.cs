using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public TransicionNegro transicion;
    
    //Solo sirve como transicio`n
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
