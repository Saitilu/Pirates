using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI UIScore;

    // Update is called once per frame
    void Update()
    {
        UIScore.text = "SCORE: " + score.ToString(); //display it in the UI text
    }
}
