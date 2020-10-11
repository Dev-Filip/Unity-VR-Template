using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]private Bullet bullet;
    [SerializeField]private Transform barrel;
    private int ammoLeft;
    private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip emptySound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammoLeft = 8;
    }

    public void TryFire()
    {
        if(ammoLeft>0)
        {
            Bullet spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.FireBullet();
            audioSource.PlayOneShot(fireSound);
            ammoLeft--;
        }
        else
        {
            audioSource.PlayOneShot(emptySound);
            Debug.Log("No more ammo in the gun");
        }
    }

    
}
