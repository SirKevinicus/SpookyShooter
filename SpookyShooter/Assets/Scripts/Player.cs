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

    // AMMO HOLDERS
    public List<AmmoHolder> ammoHolders = new List<AmmoHolder>();
    public AmmoHolder toygunAmmoHolder;

    public GameObject hud;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI ammoText;

    public delegate void OnAmmoUpdate();
    public event OnAmmoUpdate onAmmoUpdate;

    // Start is called before the first frame update
    void Awake()
    {
        fpc = GetComponent<FirstPersonAIO>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        scifiGun = FindObjectOfType<ToyGun>();

        toygunAmmoHolder = new AmmoHolder(AmmoTypes.toy, scifiGun.maxAmmoHolderAmt, scifiGun.startAmmoHolderAmt);
        ammoHolders.Add(toygunAmmoHolder);

        if(startingGun) PickUpGun(startingGun);
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

        if (Input.GetKeyDown(KeyCode.R) & equippedGun.canShoot)
        {
            StartCoroutine(equippedGun.Reload());
            onAmmoUpdate?.Invoke();
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
        foreach(AmmoHolder holder in ammoHolders)
        {
            if(holder.ammoType == type)
            {
                // If Gun accepts the ammo
                if (holder.AddAmmo(num))
                {
                    Debug.Log("Adding ammo to " + holder.ammoType + " holder" + holder.currentCapacity);
                    UpdateGunInfo();
                    onAmmoUpdate?.Invoke();

                    return true;
                }
            }
        }

        onAmmoUpdate?.Invoke();

        return false;
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
