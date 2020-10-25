using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootingGallery : MonoSingleton<ShootingGallery>
{
    public float boothWidth;
    public Transform cameraTarget;

    public Player player;
    public Camera boothCam;
    public Gun boothGun;
    public GameObject shootingUI;

    public bool playerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        boothGun = GetComponentInChildren<Gun>();
        boothCam.gameObject.SetActive(false);
        shootingUI.SetActive(false);
        playerInside = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) & !playerInside)
        {
            if((player = other.GetComponent<Player>()) != null)
            {
                StartShootingGallery();
            }
        }

        else if(Input.GetKeyDown(KeyCode.E) & playerInside)
        {
            Debug.Log("HLLO");
            EndShootingGallery();
        }
    }

    private void StartShootingGallery()
    {
        playerInside = true;
        player.DisableMovement();

        boothCam.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        shootingUI.SetActive(true);

        player.pistol.DisableGun();
        boothGun.EnableGun();
    }

    private void EndShootingGallery()
    {
        playerInside = false;
        player.EnableMovement();

        boothCam.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        shootingUI.SetActive(false);

        player.gameObject.SetActive(true);
        player.pistol.EnableGun();
        boothGun.DisableGun();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
