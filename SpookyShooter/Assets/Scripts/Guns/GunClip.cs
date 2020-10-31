using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClip : MonoBehaviour
{
    public AmmoHolder ammoHolder;

    public int maxClipAmmo;
    public int clipAmmo;

    public bool hasAmmo = true;

    public delegate void OnAmmoUpdate();
    public event OnAmmoUpdate onAmmoUpdate;

    public void Initialize(AmmoHolder ammoHolder)
    {
        this.ammoHolder = ammoHolder;
        Reload();
    }

    public void Reload()
    {
        while(ammoHolder.hasAmmo & clipAmmo < maxClipAmmo)
        {
            clipAmmo++;
            ammoHolder.SubtractAmmo(1);
            hasAmmo = true;
        }

        if (clipAmmo > 0) hasAmmo = true;
        onAmmoUpdate?.Invoke();
    }

    public bool AddAmmo(int a)
    {
        bool addedAmmo = false;
        int ammoLeft = a;

        while(clipAmmo < maxClipAmmo & ammoLeft > 0)
        {
            clipAmmo++;
            ammoLeft--;
            addedAmmo = true;
        }

        if (clipAmmo > 0) hasAmmo = true;
        onAmmoUpdate?.Invoke();

        return addedAmmo;
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

        if (clipAmmo == 0) hasAmmo = false;
        onAmmoUpdate?.Invoke();
    }
}
