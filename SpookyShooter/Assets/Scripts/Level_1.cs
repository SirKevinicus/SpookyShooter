using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : GalleryLevel
{
    public override IEnumerator LevelScript()
    {
        Debug.Log("TEST START LEVEL 1");
        spawner.SpawnTarget(0);
        yield return new WaitForSeconds(1);
        spawner.SpawnTarget(1);
    }
}
