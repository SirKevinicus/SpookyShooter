using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Camera fpsCam;
    public LayerMask hitLayers;
    public GameObject dmgDealtUIPrefab;

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
            }

            //Debug.Log(hit.transform.name);
        }
    }

    private void ShowDmgIndicator(RaycastHit hit)
    {
        if (dmgDealtUIPrefab == null)
        {
            Debug.LogError("DMG DEALT UI PREFAB NOT ASSIGNED.");
            return;
        }

        Renderer hitRend = hit.transform.GetComponentInChildren<Renderer>();
        if (hitRend == null) hitRend = hit.transform.GetComponentInParent<Renderer>();
        float hitTopBound = hitRend.bounds.extents.y;

        Debug.Log(hitTopBound);

        Vector3 dmgIndicatorPos = hit.transform.position + new Vector3(0, hitTopBound, 0);
        Instantiate(dmgDealtUIPrefab, dmgIndicatorPos, Quaternion.identity);
    }
}
