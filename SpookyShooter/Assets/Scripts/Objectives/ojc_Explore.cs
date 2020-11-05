using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ojc_Explore : Objective
{
    private Player player;
    private ShootingGallery gallery;

    public override void ActivateObjective(ObjectivesManager manager)
    {
        base.ActivateObjective(manager);

        objective_text = "Find the Shooting Gallery";

        gallery = FindObjectOfType<ShootingGallery>();
        gallery.onStartGallery += CompleteObjective;
        UpdateObjective();
    }

    protected override void CompleteObjective()
    {
        base.CompleteObjective();

        gallery.onStartGallery -= CompleteObjective;
        manager.RemoveObjective(this);
    }
}
