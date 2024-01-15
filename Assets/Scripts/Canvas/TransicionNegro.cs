using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransicionNegro : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(true));
    }
    public void StartFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool fadeIn)
    {
        //print($"fade {(fadeIn ? "in" : "out") }");
        if(fadeIn)
        {
            for (float i = 0; i <= 1; i+= Time.deltaTime) 
            {
                image.color = new(0, 0, 0, i);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                image.color = new(0, 0, 0, i);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            gameObject.SetActive(false);
        }

    }
}
