using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdle : EntityState
{
    Zombie zombie;
    public ZombieIdle(Zombie zombie)
    {
        this.zombie = zombie;
    }

    public override void OnStateEnter()
    {
        zombie.GoIdle();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (zombie.detectedPlayer)
            zombie.ChangeState(new ZombieWalk(zombie, zombie.player));

        if (zombie.isDead)
        {
            zombie.ChangeState(new ZombieDead(zombie));
        }
    }
}
