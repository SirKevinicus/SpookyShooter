using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    public int currentLevel;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void NextLevel()
    {
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 1) currentLevel++;
        else currentLevel = 0;
        SceneManager.LoadScene(currentLevel);
    }
}
