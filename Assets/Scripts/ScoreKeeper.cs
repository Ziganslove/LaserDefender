using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int scorePoints = 0;
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        ResetScore();
    }

    public void Score(int points)
    {
        scorePoints += points;
        myText.text = scorePoints.ToString();
    }

    public static void ResetScore()
    {
        scorePoints = 0;
    }
}
