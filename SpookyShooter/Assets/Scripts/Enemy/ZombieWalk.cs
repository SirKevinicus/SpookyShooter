using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWalk : EntityState
{
    public Zombie zombie;
    public Player player;

    public bool knowWherePlayerIs;
    public float lostPlayerTime = 2f;
    private float losePlayerTimer;

    public ZombieWalk(Zombie zombie, Player player)
    {
        this.zombie = zombie;
        this.player = player;
    }

    public override void OnStateEnter()
    {
        knowWherePlayerIs = true;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (!zombie.detectedPlayer && knowWherePlayerIs)
        {
            knowWherePlayerIs = false;
            losePlayerTimer = lostPlayerTime;
        }

        if (!knowWherePlayerIs)
        {
            WaitThenGiveUp();
            return;
        }

        zombie.WalkTowardsPlayer();

        if (zombie.isDead)
        {
            zombie.ChangeState(new ZombieDead(zombie));
        }
    }

    private void WaitThenGiveUp()
    {
        losePlayerTimer -= Time.deltaTime;

        if(losePlayerTimer <= 0.0f)
        {
            zombie.ChangeState(new ZombieIdle(zombie));
        }
    }
}
