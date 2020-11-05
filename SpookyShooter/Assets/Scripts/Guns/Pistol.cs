using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Camera fpsCam;
    public LayerMask hitLayers;

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), fpsCam.transform.forward);
    }

    public override void Initialize(AmmoHolder ammoHolder)
    {
        base.Initialize(ammoHolder);
        name = "Pistol";
        fpsCam = GetComponentInParent<Camera>();
    }

    public override void HandleShoot()
    {
        // check if hit an enemy
        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); // vector at the center of our cam's viewport
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range, hitLayers))
        {
            Enemy enemy;
            if ((enemy = hit.transform.GetComponent<Enemy>()) != null)
            {
                Debug.Log("HIT " + enemy.name);

                ShowDmgIndicator(hit);
                enemy.GetShot(damage);
                enemy.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * 5f, ForceMode.Impulse);
            }

            //Debug.Log(hit.transform.name);
        }
    }
}
