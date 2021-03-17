using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShooting : MonoBehaviour
{
    public bool canShoot=false;
    public GameObject bullet;
    public float startTimeBetweenShots;
    public float timeBetweenShots;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0 && canShoot) {
            GameObject shot = Instantiate(bullet, this.transform.position, this.transform.rotation);
            shot.GetComponent<BulletScript>().setBulletShooter(gameObject);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(this.gameObject.transform.GetChild(0).up * 20f, ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;

        }else if (canShoot){
            timeBetweenShots -= Time.deltaTime;
        }
    }
    public void activate(){
        canShoot = true;
    }
}
