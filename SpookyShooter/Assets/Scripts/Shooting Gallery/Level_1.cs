using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : GalleryLevel
{
    private int max_points;

    public override IEnumerator LevelScript()
    {
        spawner.SpawnTarget(0, TargetTypes.zombie);
        spawner.SpawnTarget(6, TargetTypes.zombie);

        yield return new WaitForSeconds(4);
        spawner.SpawnTarget(3, TargetTypes.zombie);
        spawner.SpawnTarget(5, TargetTypes.zombie);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(4, TargetTypes.bats);

        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(4, TargetTypes.zombie);

        yield return new WaitForSeconds(2);
        spawner.SpawnTarget(2, TargetTypes.zombie);
        spawner.SpawnTarget(5, TargetTypes.pumpkin);

        yield return new WaitForSeconds(6);

        EndLevel();
    }
}
