using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BasicEnemyShooting : MonoBehaviour
{
    public bool canShoot=false;
    public GameObject bullet;
    public float startTimeBetweenShots;
    public float timeBetweenShots;
    public AudioClip shootingSound;

    private float  changeSpeedTime = 5f;
    private float maxSpeed;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = startTimeBetweenShots + Random.Range(-0.8f, 0.8f);
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        if (this.name == "AstarTestEnemy" || this.name == "BossNinja" || this.name == "NinjaEnemy"){
            maxSpeed = this.GetComponent<AIPath>().maxSpeed;
            this.GetComponent<AIPath>().endReachedDistance = Random.Range(this.GetComponent<AIPath>().endReachedDistance -3f, this.GetComponent<AIPath>().endReachedDistance + 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots <= 0 && canShoot) {
            GameObject shot = Instantiate(bullet, this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(shootingSound);
            shot.GetComponent<BulletScript>().setBulletShooter(gameObject);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            if (this.gameObject.name != "TurretFirePoint"){
                rb.AddForce((this.gameObject.transform.GetChild(0).up * 20f) * GameManager.instance.globalTimeMult, 
                ForceMode2D.Impulse);
            } else{ // Gameobject is Turret.
                rb.AddForce((this.gameObject.transform.parent.right * 20f) * GameManager.instance.globalTimeMult, ForceMode2D.Impulse);
            }
            timeBetweenShots = startTimeBetweenShots + Random.Range(-0.2f, 0.2f);

        }else if (canShoot){
            timeBetweenShots -= Time.deltaTime;
        }
        if (this.name == "AstarTestEnemy" || this.name == "BossNinja" || this.name == "NinjaEnemy"){
            if (changeSpeedTime <= 0 && GameManager.instance.globalTimeMult >= 0.95f){
                this.GetComponent<AIPath>().maxSpeed = Random.Range(maxSpeed - 1f, maxSpeed + 1f);
                changeSpeedTime = 4f;
            }
            else{
                changeSpeedTime -= Time.deltaTime;
            }
        }
    }

    public void activate(){
        canShoot = true;
    }
}
