using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsTarget : Target
{
    public float speed;
    public int points;

    public override void Initialize(TargetSpawner spawner, Vector3 startPosition, Vector3 endPosition)
    {
        base.Initialize(spawner, startPosition, endPosition);
        my_speed = speed;
        my_points = points;
    }
}
