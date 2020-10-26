using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GalleryLevel : MonoBehaviour
{
    protected TargetSpawner spawner;
    public virtual void StartGame(TargetSpawner s)
    {
        spawner = s;
        StartCoroutine(LevelScript());
    }
    public abstract IEnumerator LevelScript();
}
