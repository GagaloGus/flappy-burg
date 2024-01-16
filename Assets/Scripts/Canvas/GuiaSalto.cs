using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiaSalto : MonoBehaviour
{
    public Sprite[] sprites;
    Image image;
    GameObject player;

    public float duration;

    private void Start()
    {
        image = GetComponent<Image>();
        player = FindObjectOfType<Player>().gameObject;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        image.sprite = sprites[0];
#elif UNITY_ANDROID
        image.sprite = sprites[1];
#endif

        StartCoroutine(FadeOut(duration));
    }


    IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(duration);
        for(float i = 1; i >= 0; i-= Time.deltaTime)
        {
            image.color = new(1, 1, 1, i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

