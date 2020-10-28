using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    FirstPersonAIO fpc;
    Rigidbody rb;
    public Camera cam;
    public Gun startingGun;
    public List<Gun> guns;

    public Gun gun;

    public GameObject hud;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        fpc = GetComponent<FirstPersonAIO>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        gun = Instantiate(startingGun, cam.transform).GetComponent<Gun>();
        guns.Add(gun);
        gun.EnableGun();
        gun.onShoot += UpdateGunInfo;
        gun.onReload += UpdateGunInfo;
    }

    public void UpdateGunInfo()
    {
        gunName.text = "" + gun.name;
        ammoText.text = "" + gun.GetCurrentAmmo() + "/" + gun.GetTotalAmmo();
    }

    // Use a Gun not equipped to the player
    public void UseGun(Gun newGun)
    {
        gun.DisableGun();
        gun = newGun;
        gun.EnableGun();
        gun.onShoot += UpdateGunInfo;
        gun.onReload += UpdateGunInfo;

        UpdateGunInfo();
    }

    public void StopUsingGun()
    {
        gun.DisableGun();
        gun = guns[0];
        gun.EnableGun();
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
