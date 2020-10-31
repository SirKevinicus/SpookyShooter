using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AmmoTypes { toy, pistol }

public class AmmoHolder
{
    public AmmoTypes ammoType;
    public int maxCapacity;
    public int currentCapacity;

    public bool hasAmmo = true;

    public AmmoHolder(AmmoTypes type, int max, int current)
    {
        this.ammoType = type;
        maxCapacity = max;
        currentCapacity = current;
    }

    public bool AddAmmo(int num)
    {
        int ammoLeft = num;
        bool ammoAdded = false;

        while(ammoLeft > 0 & currentCapacity < maxCapacity)
        {
            currentCapacity++;
            ammoLeft--;
            ammoAdded = true;
        }
        if (currentCapacity > 0) hasAmmo = true;

        return ammoAdded;
    }

    public void SubtractAmmo(int num)
    {
        int ammoLeft = num;
        while (ammoLeft > 0 & currentCapacity > 0)
        {
            currentCapacity--;
            ammoLeft--;
        }

        if (currentCapacity <= 0) hasAmmo = false;
    }
}
