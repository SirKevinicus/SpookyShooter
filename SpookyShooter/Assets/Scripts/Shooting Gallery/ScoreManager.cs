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
        TargetSpawner.instance.onTargetSpawned += AddScoreListener;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "" + 0;
    }

    // Attach a listener to the "Got Shot" event for the target
    public void AddScoreListener(Target t)
    {
        t.onGotShot += AddToScore;
    }

    public void AddToScore(Target t)
    {
        int num = t.GetPoints();
        score += num;
        scoreText.text = "" + score;
    }
}
