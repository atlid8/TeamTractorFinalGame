using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject characterAnimation;
    
    public Transform gunPoint;
    public Transform bulletRotation;


    public float bulletForce = 20f;

    private void Awake()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            Shoot();
            Debug.Log("Click to shoot!");
        }
    }

    void Shoot(){
        Debug.Log(bulletPrefab);
        Debug.Log(gunPoint);
        Debug.Log(bulletRotation);
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, bulletRotation.rotation);
        bullet.GetComponent<BulletScript>().setBulletShooter(gameObject);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletRotation.up * bulletForce, ForceMode2D.Impulse);
    }
}
