using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    private Animator animator;
    public AudioSource shotSFX;

    private float speed = 1f;
    private float startTime;

    public int points = 1;

    public float afterShotDelayTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void GetShot()
    {
        StartCoroutine(ShotActions());
    }

    private IEnumerator ShotActions()
    {
        yield return new WaitForSeconds(afterShotDelayTime);

        ScoreManager.instance.AddToScore(points);
        animator.SetTrigger("GetShot");
        if (shotSFX != null) shotSFX.Play();
        Destroy(gameObject, 2f);
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
