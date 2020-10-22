using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public LayerMask hitLayers;
    public GameObject dmgDealtUIPrefab;

    // COMPONENTS
    private Animator animator;
    private AudioSource shootSound;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.4f;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") & canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        ShootSFX(); // play gunshot
        animator.SetTrigger("Shoot"); // play shoot animation
        StartCoroutine(ShootCooldown());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, hitLayers))
        {
            if(hit.transform.GetComponent<Enemy>() != null)
            {
                ShowDmgIndicator(hit);
            }

            //Debug.Log(hit.transform.name);
        }
    }

    private void ShowDmgIndicator(RaycastHit hit)
    {
        if (dmgDealtUIPrefab == null) return;

        Renderer hitRend = hit.transform.GetComponent<Renderer>();
        float hitTopBound = hitRend.bounds.extents.y;

        Debug.Log(hitTopBound);

        Vector3 dmgIndicatorPos = hit.transform.position + new Vector3(0, hitTopBound, 0);
        Instantiate(dmgDealtUIPrefab, dmgIndicatorPos, Quaternion.identity);
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void ShootSFX()
    {
        shootSound.Play();
    }
}
