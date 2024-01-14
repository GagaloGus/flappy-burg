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

    public void StartFadeIn(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(true, duration));
    }
    public void StartFadeOut(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(false, duration));
    }

    IEnumerator Fade(bool fadeIn, float duration)
    {
        if(fadeIn)
        {
            for (float i = 0; i < 1; i+= Time.deltaTime) 
            {
                image.color = new(0, 0, 0, i);
                yield return new WaitForSeconds(Time.deltaTime * duration);
            }
        }
        else
        {
            for (float i = 1; i > 0; i -= Time.deltaTime)
            {
                image.color = new(0, 0, 0, i);
                yield return new WaitForSeconds(Time.deltaTime * duration);
            }
        }
    }
}
