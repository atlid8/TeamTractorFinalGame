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

    public GameObject muzzleFlash;

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
        }
    }

    void Shoot() {
        var position = gunPoint.position;
        var rotation = bulletRotation.rotation;
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        var muzzle = Instantiate(muzzleFlash, position, rotation);
        StartCoroutine(Commons.FadeAway(muzzle.GetComponentInChildren<SpriteRenderer>(), 0.06f));
        StartCoroutine(Commons.DelayedAction(() => Destroy(muzzle), 0.1f));
        var bulletScript = bullet.GetComponent<BulletScript>(); 
        bulletScript.setBulletShooter(gameObject);
        bulletScript.playerBullet = true;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletRotation.up * bulletForce, ForceMode2D.Impulse);
        
        GameManager.instance.AddTimeEffect(true);
        StartCoroutine(Commons.DelayedAction(() =>
        {
            GameManager.instance.AddTimeEffect(false);
        }, 5f));
    }
}
