using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunTypes { pistol, scifi }

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GunClip))]
public abstract class Gun : MonoBehaviour
{
    // COMPONENTS
    private Animator animator;
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    public GunClip clip;
    private AudioSource audioSource;

    // REFERENCES
    public AmmoHolder ammoHolder;
    public GameObject dmgDealtUIPrefab;

    public int damage = 10;
    public float range = 100f;
    public float fireRate = 0.4f;
    public GunTypes gunType = GunTypes.pistol;
    public AmmoTypes ammoType = AmmoTypes.pistol;
    public int ammoPerShot = 1;
    public float reloadTime = 1f;
    public int startAmmoHolderAmt = 36;
    public int maxAmmoHolderAmt = 36;

    public bool isActiveGun = false;
    public bool canShoot;

    public delegate void OnShoot();
    public event OnShoot onShoot;

    public delegate void OnReload();
    public event OnReload onReload;

    public virtual void Initialize(AmmoHolder ammoHolder)
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        this.ammoHolder = ammoHolder;

        clip = GetComponent<GunClip>();
        clip.Initialize(ammoHolder);

        canShoot = true;
    }

    public IEnumerator Reload()
    {
        canShoot = false;

        if (!clip.hasAmmo & !ammoHolder.hasAmmo)
        {
            GunEmpty();
            yield break;
        }

        PlayReloadSFX();

        yield return new WaitForSeconds(reloadTime);

        clip.Reload();

        onReload?.Invoke();

        canShoot = true;
    }

    public IEnumerator Shoot()
    {
        canShoot = false;

        ShootSFX(); // play gunshot

        // subtract ammo
        clip.SubtractAmmo(ammoPerShot);

        // play shoot animation
        animator.SetTrigger("Shoot");

        HandleShoot();

        onShoot?.Invoke();

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void GunEmpty()
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

    public void PlayReloadSFX()
    {
        if (reloadSound)
            audioSource.clip = reloadSound;
        audioSource.Play();
    }

    private void ShootSFX()
    {
        if (shootSound != null) 
            audioSource.clip = shootSound;
       
        audioSource.Play();
    }

    public int GetClipAmmo()
    {
        return clip.clipAmmo;
    }

    public int GetHolderAmmo()
    {
        return ammoHolder.currentCapacity;
    }

    public int GetTotalAmmo()
    {
        return ammoHolder.currentCapacity + clip.clipAmmo;
    }

    public AmmoTypes GetAmmoType()
    {
        return ammoType;
    }

    public void ShowDmgIndicator(RaycastHit hit)
    {
        DmgDealtIndicator ind = Instantiate(dmgDealtUIPrefab).GetComponent<DmgDealtIndicator>();
        ind.Initialize(damage, hit);
    }
}
