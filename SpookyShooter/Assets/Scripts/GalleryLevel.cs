using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GalleryLevel : MonoBehaviour
{
    protected TargetSpawner spawner;

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

        StartCoroutine(LevelScript());
    }

    public virtual void EndLevel()
    {
        if (ScoreManager.instance.score >= pointsToWin)
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
