using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TargetTypes { zombie, pumpkin, bats }

public class TargetSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public ShootingGallery gallery;

    private List<Target> targets = new List<Target>();

    public GameObject zombie_prefab;
    public GameObject pumpkin_prefab;
    public GameObject bats_prefab;

    public float afterShotDelayTime = 0.2f;

    public delegate void TargetSpawned(Target t);
    public event TargetSpawned onTargetSpawned;


    // Start is called before the first frame update
    void Start()
    {
        gallery = GetComponentInParent<ShootingGallery>();
    }

    public void SpawnTarget(int spawn, TargetTypes type)
    {
        if (spawn > spawnPoints.Length)
        {
            Debug.LogError("Spawn Location " + spawn + " Not Valid");
            return;
        }
        Vector3 position = spawnPoints[spawn].localPosition;
        int direction;
        if (spawn % 2 == 0) direction = 1;
        else direction = -1;

        SpawnTarget(position, direction, type);
    }

    void SpawnTarget(Vector3 position, int direction, TargetTypes type)
    {
        Vector3 endPosition = position + (Vector3.right * gallery.boothWidth * direction);
        GameObject prefab;

        switch(type)
        {
            case TargetTypes.zombie:
                prefab = zombie_prefab;
                break;
            case TargetTypes.pumpkin:
                prefab = pumpkin_prefab;
                break;
            case TargetTypes.bats:
                prefab = bats_prefab;
                break;
            default:
                Debug.LogError("Target Type does not exist.");
                prefab = null;
                break;
        }

        Target[] targets = Instantiate(prefab, transform).GetComponentsInChildren<Target>();
        foreach(Target t in targets)
        {
            t.Initialize(this, position, endPosition);
            this.targets.Add(t); // Add to list of targets
            onTargetSpawned?.Invoke(t);
        }
    }

    public void DeleteTargets()
    {
        foreach(Target t in targets)
        {
            if(t) Destroy(t.gameObject);
        }
        targets.Clear();
    }
}
