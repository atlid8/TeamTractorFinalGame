using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Shooting : MonoBehaviour
{
    public bool canShoot=false;
    private bool whichArm=true;
    public GameObject bullet;

    public GameObject firePoint1;
    public GameObject firePoint2;

    public float startTimeBetweenShots;
    public float timeBetweenShots;
    public AudioClip bossShootingSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0 && canShoot && whichArm) {
            audioSource.PlayOneShot(bossShootingSound);
            GameObject shot = Instantiate(bullet, firePoint1.transform.position, firePoint1.transform.rotation);
            shot.GetComponent<BulletScript>().setBulletShooter(gameObject);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce((this.gameObject.transform.GetChild(0).up * 20f) * GameManager.instance.globalTimeMult, 
                ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;
            whichArm = false;
        }
        else if (timeBetweenShots <= 0 && canShoot && !whichArm){
            GameObject shot = Instantiate(bullet, firePoint2.transform.position, firePoint2.transform.rotation);
            shot.GetComponent<BulletScript>().setBulletShooter(gameObject);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce((this.gameObject.transform.GetChild(0).up * 20f) * GameManager.instance.globalTimeMult, 
                ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;
            whichArm = true;
        }
        else if (canShoot){
            timeBetweenShots -= Time.deltaTime;
        }
    }
    public void activate(){
        canShoot = true;
    }
}
