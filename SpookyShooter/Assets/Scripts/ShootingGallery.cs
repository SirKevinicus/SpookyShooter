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
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        boothGun = GetComponentInChildren<Gun>();
        boothCam.gameObject.SetActive(false);
        shootingUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((player = other.GetComponent<Player>()) != null)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((player = other.GetComponent<Player>()) != null)
        {
            player = null;
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) & playerInRange)
        {
            if(!playerInside)
            {
                StartShootingGallery();
            }
            else
            {
                EndShootingGallery();
            }
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
}
