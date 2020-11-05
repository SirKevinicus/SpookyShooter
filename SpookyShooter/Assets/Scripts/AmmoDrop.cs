using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public AmmoTypes type;
    private AudioSource audioSource;
    public AudioClip pickupSound;
    public int num;
    public bool pickedUp = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = pickupSound;
    }

    public void Initialize(AmmoTypes type, int num)
    {
        this.type = type;
        this.num = num;
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player;
        if (!pickedUp & (player = other.transform.GetComponent<Player>()))
        {
            pickedUp = player.PickUpAmmo(type, num);
            if (pickedUp)
            {
                audioSource.Play();
                Destroy(gameObject, 1f);
            }
        }
    }
}
