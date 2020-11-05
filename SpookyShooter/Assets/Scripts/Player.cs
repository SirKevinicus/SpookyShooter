using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // COMPONENTS
    FirstPersonAIO fpc;
    Rigidbody rb;
    public Camera cam;

    // REFERNCES
    public ToyGun scifiGun;

    // GUNS
    public Gun startingGun;
    public Gun equippedGun;

    public List<Gun> guns;
    public List<AmmoHolder> ammoHolders = new List<AmmoHolder>();

    // UI
    public GameObject hud;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI ammoText;
    public GameObject reloadHUD;

    public delegate void OnAmmoUpdate();
    public event OnAmmoUpdate onAmmoUpdate;

    // Start is called before the first frame update
    void Awake()
    {
        fpc = GetComponent<FirstPersonAIO>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        scifiGun = FindObjectOfType<ToyGun>();

        if(startingGun) PickUpGun(startingGun);

        reloadHUD.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") & equippedGun.canShoot)
        {
            if (equippedGun.clip.hasAmmo)
            {
                StartCoroutine(equippedGun.Shoot());
                onAmmoUpdate?.Invoke();
            }
            else
            {
                equippedGun.GunEmpty();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(equippedGun.Reload());
            onAmmoUpdate?.Invoke();
        }

        if(!equippedGun.clip.hasAmmo)
        {
            reloadHUD.SetActive(true);
        } 
        else
        {
            reloadHUD.SetActive(false);
        }
    }

    public bool PickUpGun(Gun newGun)
    {
        if(!guns.Contains(newGun))
        {
            AmmoTypes ammoType = newGun.GetAmmoType();
            AmmoHolder thisGunsAmmo;

            // If there is already a AmmoHolder for this gun's ammo type, use it
            if(ammoHolders.Exists(x => x.ammoType == ammoType))
            {
                thisGunsAmmo = ammoHolders.Find(x => x.ammoType == ammoType);
            }
            // Otherwise, make a new AmmoHolder for this ammo type
            else
            {
                thisGunsAmmo = new AmmoHolder(ammoType, newGun.startAmmoHolderAmt, newGun.maxAmmoHolderAmt);
                ammoHolders.Add(thisGunsAmmo);
            }

            Gun gun = Instantiate(newGun, cam.transform).GetComponent<Gun>();
            gun.Initialize(thisGunsAmmo);

            guns.Add(gun);
            equippedGun = gun;
            gun.EnableGun();

            gun.onShoot += UpdateGunInfo;
            gun.onReload += UpdateGunInfo;
            UpdateGunInfo();

            return true;
        }
        return false;
    }

    public bool PickUpAmmo(AmmoTypes type, int num)
    {
        //Debug.Log("Picked up " + num + " " + type + " ammo.");
        bool acceptedAmmo = false;

        // Add ammo to the clips in the guns first
        foreach(Gun gun in guns)
        {
            if(gun.ammoType == type)
            {
                int ammoAdded = gun.clip.AddAmmo(num);
                {
                    if (ammoAdded > 0)
                    {
                        //Debug.Log("Added " + ammoAdded + " ammo to " + gun.name);
                        num -= ammoAdded;
                        acceptedAmmo = true;
                    }
                }
            }
        }

        // Then add ammo to the AmmoHolders
        foreach(AmmoHolder holder in ammoHolders)
        {
            if (holder.ammoType == type)
            {
                // If Holder accepts the ammo
                if (holder.AddAmmo(num))
                {
                    //Debug.Log("Added " + num + " ammo to " + holder.ammoType + " holder");
                    acceptedAmmo = true;
                }
            }
        }

        UpdateGunInfo();
        onAmmoUpdate?.Invoke();

        return acceptedAmmo;
    }

    public void UpdateGunInfo()
    {
        gunName.text = "<bounce>" + equippedGun.name + "</bounce>";
        ammoText.text = "" + equippedGun.GetClipAmmo() + "/" + equippedGun.GetHolderAmmo();
    }

    // Use a Gun not equipped to the player
    public void UseGun(Gun newGun)
    {
        equippedGun.DisableGun();
        equippedGun = newGun;
        equippedGun.EnableGun();

        if(!guns.Contains(newGun))
        {
            guns.Add(newGun);
            equippedGun.onShoot += UpdateGunInfo;
            equippedGun.onReload += UpdateGunInfo;
        }

        UpdateGunInfo();
    }

    public void PickUpAmmoHolder(AmmoHolder holder)
    {
        // If the player doesn't already have a holder of this type
        if (!ammoHolders.Exists(x => x.ammoType == holder.ammoType))
        {
            // add it to the player's holders
            ammoHolders.Add(holder);
        }
    }

    public void StopUsingGun()
    {
        equippedGun.DisableGun();
        equippedGun = guns[0];
        equippedGun.EnableGun();
        UpdateGunInfo();
        //gun.onShoot += UpdateGunInfo;
        //gun.onReload += UpdateGunInfo;
    }

    public void DisableMovement()
    {
        fpc.playerCanMove = false;
        fpc.enableCameraMovement = false;
        cam.gameObject.SetActive(false);
    }

    public void EnableMovement()
    {
        fpc.playerCanMove = true;
        fpc.enableCameraMovement = true;
        cam.gameObject.SetActive(true);
    }
}
