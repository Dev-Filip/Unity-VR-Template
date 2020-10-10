using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]private Bullet bullet;
    [SerializeField]private Transform barrel;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip emptySound;
    [SerializeField] private AudioSource audioSource;
    private int ammo;
    // Start is called before the first frame update
    void Start()
    {
        ammo = 14;
    }

    public void TryFire()
    {
        if(ammo>0)
        {
            Bullet spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            ammo--;
            spawnedBullet.Launch();
            audioSource.PlayOneShot(fireSound);

            Debug.Log("Bullet launched");
        }
        else
        {
            audioSource.PlayOneShot(emptySound);
            Debug.Log("There is no ammo in the gun");
        }
    }

}
