using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject characterAnimation;
    
    private Transform gunPoint;
    public Transform bulletRotation;


    public float bulletForce = 20f;

    private void Awake()
    {
        gunPoint = characterAnimation.GetComponent<CharacterAnimation>().GetGunPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButton("Jump"))
        {
            Shoot();
        }
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, bulletRotation.rotation);
        bullet.GetComponent<BulletScript>().setBulletShooter(gameObject);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletRotation.up * bulletForce, ForceMode2D.Impulse);
    }
}
