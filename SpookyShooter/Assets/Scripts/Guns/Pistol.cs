using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Camera fpsCam;
    public LayerMask hitLayers;

    public override void Initialize()
    {
        base.Initialize();
        fpsCam = GetComponentInParent<Camera>();
    }

    public override void HandleShoot()
    {
        // check if hit an enemy
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, hitLayers))
        {
            Debug.Log("HIT");

            Enemy enemy;
            if ((enemy = hit.transform.GetComponentInParent<Enemy>()) != null)
            {
                ShowDmgIndicator(hit);
                enemy.GetShot(damage);
            }

            //Debug.Log(hit.transform.name);
        }
    }
}
