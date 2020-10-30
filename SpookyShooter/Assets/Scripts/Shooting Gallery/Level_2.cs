using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2 : GalleryLevel
{
    public override IEnumerator LevelScript()
    {
        spawner.SpawnTarget(0, TargetTypes.zombie);
        spawner.SpawnTarget(3, TargetTypes.zombie);
        spawner.SpawnTarget(7, TargetTypes.zombie);
        spawner.SpawnTarget(4, TargetTypes.zombie);

        yield return new WaitForSeconds(5);
        spawner.SpawnTarget(1, TargetTypes.bats);
        spawner.SpawnTarget(0, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(4, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(0, TargetTypes.pumpkin);
        spawner.SpawnTarget(6, TargetTypes.bats);

        yield return new WaitForSeconds(2);
        spawner.SpawnTarget(0, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(2, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(4, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(6, TargetTypes.zombie);

        yield return new WaitForSeconds(6);
        spawner.SpawnTarget(6, TargetTypes.bats);

        yield return new WaitForSeconds(6);

        EndLevel();
    }
}
