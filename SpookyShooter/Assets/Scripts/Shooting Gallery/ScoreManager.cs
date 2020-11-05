using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    private TargetSpawner targetSpawner;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        targetSpawner = FindObjectOfType<TargetSpawner>().GetComponent<TargetSpawner>();
        targetSpawner.onTargetSpawned += AddScoreListener;
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
