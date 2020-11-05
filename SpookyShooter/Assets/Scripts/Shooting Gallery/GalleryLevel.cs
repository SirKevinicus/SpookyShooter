using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GalleryLevel : MonoBehaviour
{
    protected TargetSpawner spawner;
    protected ScoreManager scoreManager;

    public AudioClip beatLevelSound;

    public int pointsToWin;
    public int maxScore;

    public delegate void BeatLevel();
    public event BeatLevel onBeatLevel;

    public delegate void LoseLevel();
    public event LoseLevel onLoseLevel;

    public virtual void StartLevel(TargetSpawner s)
    {
        spawner = s;
        scoreManager = FindObjectOfType<ScoreManager>();

        StartCoroutine(LevelScript());
    }

    public virtual void EndLevel()
    {
        if (scoreManager.score >= pointsToWin)
        {
            Debug.LogError("YOU BEAT LEVEL");
            onBeatLevel?.Invoke();
        }
        else
        {
            onLoseLevel?.Invoke();
        }
    }

    public abstract IEnumerator LevelScript();
}
