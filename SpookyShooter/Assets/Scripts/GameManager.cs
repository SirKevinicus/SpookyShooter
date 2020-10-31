using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    ShootingGallery shootingGallery;

    public ObjectivesManager ojcManager;
    public Objective collectAmmo_ojc;


    // Start is called before the first frame update
    void Start()
    {
        shootingGallery = FindObjectOfType<ShootingGallery>();
        ojcManager = FindObjectOfType<ObjectivesManager>();

        ojcManager.CreateObjective(collectAmmo_ojc, 18);        
        //shootingGallery.onBeatLevel += ShowAmmoObjective;
    }

    private void ShowAmmoObjective()
    {
        if(shootingGallery.currentLevelNum == 1)
            ojcManager.CreateObjective(collectAmmo_ojc, 18);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
