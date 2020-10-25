using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Gun : MonoBehaviour
{
    // COMPONENTS
    private Animator animator;
    private AudioSource shootSound;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.4f;

    public bool isActiveGun = false;
    private bool canShoot;

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        shootSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") & isActiveGun & canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        ShootSFX(); // play gunshot

        // play shoot animation
        animator.SetTrigger("Shoot");

        // trigger cooldown
        StartCoroutine(ShootCooldown()); 

        HandleShoot();
    }

    public void DisableGun()
    {
        isActiveGun = false;
    }

    public void EnableGun()
    {
        isActiveGun = true;
    }

    public abstract void HandleShoot();

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void ShootSFX()
    {
        if(shootSound != null) shootSound.Play();
    }
}
