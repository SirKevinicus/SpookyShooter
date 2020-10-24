using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGallery : MonoSingleton<ShootingGallery>
{
    public float boothWidth;
    public Transform cameraTarget;

    public Player player;
    public Camera boothCam;
    public GameObject shootingUI;

    // Start is called before the first frame update
    void Start()
    {
        boothCam.gameObject.SetActive(false);
        boothCam.enabled = false;
        shootingUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((player = other.GetComponent<Player>()) != null)
        {
            player.gameObject.SetActive(false);
            StartShootingGallery();
        }
    }

    private void StartShootingGallery()
    {
        boothCam.gameObject.SetActive(true);
        boothCam.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        shootingUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
