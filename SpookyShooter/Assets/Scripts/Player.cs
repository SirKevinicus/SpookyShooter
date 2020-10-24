using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    FirstPersonAIO fpc;
    Rigidbody rb;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        fpc = GetComponent<FirstPersonAIO>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableMovement()
    {
        fpc.playerCanMove = false;
        rb.isKinematic = true;
        fpc.enableCameraMovement = false;
    }

    public void EnableMovement()
    {
        fpc.playerCanMove = true;
        rb.isKinematic = false;
        fpc.enableCameraMovement = true;
    }
}
