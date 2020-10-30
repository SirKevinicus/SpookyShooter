using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Crosshair : MonoBehaviour
{
    public Gun myGun;

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
