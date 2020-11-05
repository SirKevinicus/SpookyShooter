using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Zombie : Enemy
{
    // VARIABLES
    [SerializeField] private float m_moveSpeed = 2;
    private readonly float m_interpolation = 10f;

    // COMPONENTS
    [SerializeField] public Animator animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;
    [SerializeField] private ConeCollider detect_cone = null;

    // STATE
    public bool freeze = false;
    public EntityState currentState;
    public delegate void OnDie(Zombie z);
    public event OnDie onDie;

    // REFERENCES
    public Player player;
    public bool detectedPlayer = false;


    private Vector3 m_currentDirection = Vector3.zero;

    private void Awake()
    {
        if (!animator) { GetComponentInChildren<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Rigidbody>(); }
        if (!detect_cone) { GetComponentInChildren<ConeCollider>(); }

        SetColliderState(false);
        SetRigidbodyState(true);

        currentState = new ZombieIdle(this);
    }

    public void Initialize()
    {
        
    }

    private void Update()
    {
        if(!freeze) currentState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        if(!freeze) currentState.OnStateFixedUpdate();
    }

    private void LateUpdate()
    {
        if(!freeze) transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
        animator.enabled = false;
        SetColliderState(true);
        SetRigidbodyState(false);
        onDie?.Invoke(this);
        GetComponent<DropAmmo>().SpawnAmmoDrop(transform.position);
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

    private void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    private void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider coll in colliders)
        {
            coll.enabled = state;
        }

        Collider[] parentColliders = GetComponents<Collider>();
        foreach(Collider c in parentColliders)
        {
            c.enabled = !state;
        }
    }
}
