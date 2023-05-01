using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI UIScore;

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Canvas"));
    }
    // Update is called once per frame
    void Update()
    {
        UIScore.text = "Treasure: " + score.ToString(); //display it in the UI text
    }
    public void CollectTreasure()
    {
        score++;
    }
}
