using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    // Set by Target Spawner
    private TargetSpawner spawner;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;

    // Components
    private Animator animator;
    public AudioSource shotSFX;

    // Variables
    protected float my_speed = 1f;
    protected int my_points = 1;

    // State
    private bool hasBeenShot;

    public delegate void GotShot(Target t);
    public event GotShot onGotShot;

    public virtual void Initialize(TargetSpawner spawner, Vector3 startPosition, Vector3 endPosition)
    {
        this.spawner = spawner;
        this.startPosition = startPosition;
        transform.localPosition = startPosition;
        this.endPosition = endPosition;

        animator = GetComponent<Animator>();

        startTime = Time.time;
        hasBeenShot = false;

    }

    public void GetShot()
    {
        if(!hasBeenShot)
            StartCoroutine(ShotActions());
        hasBeenShot = true;
    }

    public void MadeToEnd()
    {
        animator.SetTrigger("GetShot");
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator ShotActions()
    {
        yield return new WaitForSeconds(spawner.afterShotDelayTime);

        animator.SetTrigger("GetShot");
        if (shotSFX != null) shotSFX.Play();
        onGotShot?.Invoke(this);
        Destroy(gameObject, 2f);
    }

    public int GetPoints()
    {
        return my_points;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(startPosition, endPosition, (my_speed / 10f) * (Time.time - startTime));
        transform.localPosition = newPos;

        // Check if reached the end
        float distance = Mathf.Abs((endPosition - transform.localPosition).x);
        if(distance <= 0.01f)
        {
            MadeToEnd();
        }
    }
}
