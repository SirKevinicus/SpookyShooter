using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Ammo))]
public abstract class Gun : MonoBehaviour
{
    // COMPONENTS
    private Animator animator;
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    private Ammo ammo;

    private AudioSource audioSource;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.4f;
    public int ammoPerShot = 1;
    public float reloadTime = 1f;

    public bool isActiveGun = false;
    public bool canShoot;

    public delegate void OnShoot();
    public event OnShoot onShoot;

    public delegate void OnReload();
    public event OnReload onReload;

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        ammo = GetComponent<Ammo>();

        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveGun)
        {
            if (Input.GetButtonDown("Fire1") & canShoot)
            {
                if (ammo.hasClipAmmo)
                {
                    Shoot();
                }
                else
                {
                    GunEmpty();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }
    }

    public IEnumerator Reload()
    {
        if(!ammo.hasAmmo)
        {
            GunEmpty();
            yield break;
        }
        if (reloadSound != null)
        {
            audioSource.clip = reloadSound;
            audioSource.Play();
        }

        canShoot = false;

        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
        ammo.Reload();
        onReload?.Invoke();
    }

    void Shoot()
    {
        ShootSFX(); // play gunshot

        // subtract ammo
        ammo.SubtractAmmo(ammoPerShot);

        // play shoot animation
        animator.SetTrigger("Shoot");

        // trigger cooldown
        StartCoroutine(ShootCooldown()); 

        HandleShoot();

        onShoot?.Invoke();
    }

    protected void GunEmpty()
    {
        if (emptySound != null) audioSource.clip = emptySound;
        audioSource.Play();
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
        if (shootSound != null) 
            audioSource.clip = shootSound;
       
        audioSource.Play();
    }

    public int GetCurrentAmmo()
    {
        return ammo.clipAmmo;
    }

    public int GetTotalAmmo()
    {
        return ammo.currentAmmo;
    }
}
