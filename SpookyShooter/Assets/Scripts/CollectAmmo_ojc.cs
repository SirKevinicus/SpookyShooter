using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAmmo_ojc : Objective
{
    private Player player;
    private ShootingGallery gallery;

    private int ammoToCollect;

    public override void ActivateObjective(Player player, int ammoToCollect)
    {
        base.ActivateObjective(player, ammoToCollect);

        gallery = FindObjectOfType<ShootingGallery>();
        this.player = player;

        player.onAmmoUpdate += UpdateObjective;
        this.ammoToCollect = ammoToCollect;

        UpdateObjective();
    }
    protected override void UpdateObjective()
    {
        Debug.Log("UPDATE OBJECTIVE");
        objective_text = "Collect Shooting Gallery Ammo: " + gallery.boothGun.GetTotalAmmo() + "/" + ammoToCollect;

        if(gallery.boothGun.GetTotalAmmo() >= ammoToCollect)
        {
            CompleteObjective();
        }

        base.UpdateObjective();
    }
}
