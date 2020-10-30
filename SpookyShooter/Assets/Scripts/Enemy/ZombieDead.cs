using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDead : EntityState
{
    public Zombie zombie;
    public Player player;

    public ZombieDead(Zombie zombie)
    {
        this.zombie = zombie;
    }

    public override void OnStateEnter()
    {
        zombie.animator.SetTrigger("Dead");
        zombie.WaitThenDelete();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }

}
