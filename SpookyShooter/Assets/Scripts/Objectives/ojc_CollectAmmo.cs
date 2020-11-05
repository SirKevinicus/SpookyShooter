using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ojc_CollectAmmo : Objective
{
    private Player player;
    private ShootingGallery gallery;

    private int ammoToCollect;

    public override void ActivateObjective(ObjectivesManager manager, int ammoToCollect)
    {
        base.ActivateObjective(manager, ammoToCollect);

        gallery = FindObjectOfType<ShootingGallery>();
        this.player = FindObjectOfType<Player>();

        player.onAmmoUpdate += UpdateObjective;
        this.ammoToCollect = ammoToCollect;

        UpdateObjective();
    }
    protected override void UpdateObjective()
    {
        objective_text = "Collect Shooting Gallery Ammo: " + gallery.scifiGun.GetTotalAmmo() + "/" + ammoToCollect;

        base.UpdateObjective();
    }

    protected override void CompleteObjective()
    {
        base.CompleteObjective();

        player.onAmmoUpdate -= UpdateObjective;
        manager.RemoveObjective(this);
    }
}
