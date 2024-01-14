using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TMP_Text pointText, highscoreText;
    // Start is called before the first frame update
    void Start()
    {
        List<int> Top3 = GameManager.instance.highScore;
        Top3.Sort(); Top3.Reverse();

        highscoreText.text = 
            $"highscore" +
            $"\n1.{(Top3.Count <= 0 ? "" : Top3[0])}" +
            $"\n2.{(Top3.Count <= 1 ? "" : Top3[1])}" +
            $"\n3.{(Top3.Count <= 2 ? "" : Top3[2])}";
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = $"Points {GameManager.instance.points}";
    }
}
