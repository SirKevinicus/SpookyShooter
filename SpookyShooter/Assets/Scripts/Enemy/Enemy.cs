using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float health = 100f;
    public bool isDead = false;

    public void GetShot(float damage)
    {
        health -= damage;
        if (health <= 0) isDead = true;
    }
}
