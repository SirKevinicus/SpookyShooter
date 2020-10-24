using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float speed = 1f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetShot()
    {
        Debug.Log("SHOT ME!");
    }

    public void Initialize(Vector3 startPosition, Vector3 endPosition)
    {
        this.startPosition = startPosition;
        transform.localPosition = startPosition;
        this.endPosition = endPosition;

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(startPosition, endPosition, (speed / 10f) * (Time.time - startTime));
        transform.localPosition = newPos;
    }
}
