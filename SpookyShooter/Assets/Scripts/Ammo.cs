using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int maxAmmo;
    public int maxClipAmmo;
    public int startAmmo;
    public int startClipAmmo;
    public int clipAmmo;
    public int currentAmmo;

    public bool hasClipAmmo = true;
    public bool hasAmmo = true;

    public void Start()
    {
        currentAmmo = startAmmo;
        clipAmmo = startClipAmmo;
    }

    public void Reload()
    {
        while(currentAmmo > 0 & clipAmmo < maxClipAmmo)
        {
            clipAmmo++;
            currentAmmo--;
            hasClipAmmo = true;
        }

        if (currentAmmo == 0) hasAmmo = false;
    }

    public void AddAmmo(int a)
    {
        int ammoLeft = a;

        while(clipAmmo < maxClipAmmo & ammoLeft > 0)
        {
            clipAmmo++;
            ammoLeft--;
        }
        
        while(currentAmmo < maxAmmo & ammoLeft > 0)
        {
            currentAmmo++;
            ammoLeft--;
        }

        if(currentAmmo > 0) hasAmmo = true;
        if (clipAmmo > 0) hasClipAmmo = true;
    }

    public void SubtractAmmo(int a)
    {
        int ammoLeft = a;

        // Subtract from clip
        while(clipAmmo > 0 & ammoLeft > 0)
        {
            clipAmmo--;
            ammoLeft--;
        }

        if (clipAmmo == 0) hasClipAmmo = false;
    }
}
