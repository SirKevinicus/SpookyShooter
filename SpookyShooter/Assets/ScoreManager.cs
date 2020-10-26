using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddToScore(int num)
    {
        score += num;
        scoreText.text = "" + score;
    }
}
