using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    ShootingGallery shootingGallery;
    ZombieSpawner zombieSpawner;

    public ObjectivesManager ojcManager;
    public Objective collectAmmo_ojc;
    public Objective ojc_explore;

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnGameStart;
    }

    public void OnGameStart(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            shootingGallery = FindObjectOfType<ShootingGallery>();
            zombieSpawner = FindObjectOfType<ZombieSpawner>();
            ojcManager = FindObjectOfType<ObjectivesManager>();
            ojcManager.ShowExploreObjective();

            shootingGallery.onBeatLevel += zombieSpawner.StartSpawningZombies;
            shootingGallery.onStartGallery += zombieSpawner.StopSpawningZombies;
            shootingGallery.onStartGallery += zombieSpawner.FreezeAllZombies;
            shootingGallery.onEndGallery += zombieSpawner.UnFreezeAllZombies;
            shootingGallery.onBeatLevel += ShowAmmoObjective;
        }
    }

    private void ShowAmmoObjective()
    {
        if(shootingGallery.currentLevelNum == 1)
            ojcManager.CreateObjective(collectAmmo_ojc, 18);
    }
}
