using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePos, out hit))
            {
                Target target;
                if((target = hit.transform.GetComponentInParent<Target>()) != null)
                {
                    target.GetShot();
                }
            }
        }

    }
}
