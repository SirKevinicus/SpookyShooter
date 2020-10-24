using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject targetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnTargets", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTargets()
    {
        SpawnTarget(spawnPoints[0].localPosition);
    }

    void SpawnTarget(Vector3 position)
    {
        Vector3 endPosition = position + (Vector3.right * ShootingGallery.instance.boothWidth);

        Target target = Instantiate(targetPrefab, transform).GetComponent<Target>();
        target.Initialize(position, endPosition);
    }
}
