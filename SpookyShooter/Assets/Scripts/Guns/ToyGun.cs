using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGun : Gun
{
    public GameObject crosshair;

    public override void HandleShoot()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mousePos, out hit))
        {
            Target target;
            if ((target = hit.transform.GetComponentInParent<Target>()) != null)
            {
                target.GetShot();
            }
        }
    }
}
