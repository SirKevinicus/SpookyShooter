using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3 : GalleryLevel
{
    public override IEnumerator LevelScript()
    {
        spawner.SpawnTarget(0, TargetTypes.zombie);
        spawner.SpawnTarget(1, TargetTypes.zombie);
        spawner.SpawnTarget(2, TargetTypes.zombie);
        spawner.SpawnTarget(3, TargetTypes.zombie);
        spawner.SpawnTarget(6, TargetTypes.bats);

        yield return new WaitForSeconds(3);
        spawner.SpawnTarget(1, TargetTypes.pumpkin);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(5, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(1, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(5, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(2, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(5, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(3, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(1, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(6, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(1, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(4, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(2, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(5, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(0, TargetTypes.bats);

        yield return new WaitForSeconds(6);
        spawner.SpawnTarget(7, TargetTypes.pumpkin);

        yield return new WaitForSeconds(2);
        spawner.SpawnTarget(4, TargetTypes.pumpkin);

        yield return new WaitForSeconds(2);
        spawner.SpawnTarget(1, TargetTypes.pumpkin);

        yield return new WaitForSeconds(6);

        EndLevel();
    }
}
