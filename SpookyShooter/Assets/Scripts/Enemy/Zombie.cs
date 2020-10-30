using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    [SerializeField] public Animator animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;
    [SerializeField] private ConeCollider detect_cone = null;

    public EntityState currentState;

    public Player player;
    public bool detectedPlayer = false;

    private readonly float m_interpolation = 1f;

    private Vector3 m_currentDirection = Vector3.zero;

    private void Awake()
    {
        if (!animator) { GetComponentInChildren<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Rigidbody>(); }
        if (!detect_cone) { GetComponent<ConeCollider>(); }

        currentState = new ZombieIdle(this);
    }

    private void Update()
    {
        currentState.OnStateUpdate();

        if(isDead)
        {
            ChangeState(new ZombieDead(this));
        }
    }

    private void FixedUpdate()
    {
        currentState.OnStateFixedUpdate();
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    public void WalkTowardsPlayer()
    {
        if(!player)
        {
            Debug.LogError("Player reference is missing.");
            return;
        }

        Vector3 playerPos = player.transform.position;

        // Rotate Towards Player
        Quaternion targetRot = Quaternion.LookRotation(playerPos - transform.position);
        float interp = Mathf.Min(m_interpolation * Time.deltaTime, 1);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, interp);

        animator.SetFloat("MoveSpeed", 1.0f);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerPos, Time.deltaTime * (m_moveSpeed/10));
    }

    public void GoIdle()
    {
        animator.SetFloat("MoveSpeed", 0.0f);
    }

    public void WaitThenDelete()
    {
        StartCoroutine(IWaitThenDelete());
    }

    private IEnumerator IWaitThenDelete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    public void ChangeState(EntityState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<Player>()) != null)
        {
            player = other.GetComponent<Player>();
            detectedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.GetComponent<Player>()) != null)
        {
            player = null;
            detectedPlayer = false;
        }
    }
}
