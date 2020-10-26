using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject targetPrefab;
    public ShootingGallery gallery;

    // Start is called before the first frame update
    void Start()
    {
        gallery = GetComponentInParent<ShootingGallery>();
    }

    public void SpawnTarget(int spawn)
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

        SpawnTarget(position, direction);
    }

    void SpawnTarget(Vector3 position, int direction)
    {
        Vector3 endPosition = position + (Vector3.right * gallery.boothWidth * direction);

        Target target = Instantiate(targetPrefab, transform).GetComponent<Target>();
        target.Initialize(position, endPosition);
    }
}
