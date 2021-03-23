using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilities : MonoBehaviour
{
    public GameObject bulletPrefab;
    private BasicEnemyShooting shooting;

    private int numberOfProjectiles = 20;
    private float startTimeBetweenCircleAttack = 15f;
    private float timeBetweenCircleAttack;

    private float starttimeBetweenRapidFire = 10f;
    private float timeBetweenRapidFire;
    // Start is called before the first frame update
    void Start()
    {
        shooting = gameObject.GetComponent<BasicEnemyShooting>();
        timeBetweenRapidFire = starttimeBetweenRapidFire;
        timeBetweenCircleAttack = startTimeBetweenCircleAttack;        
    }

    void Update() {
        timeBetweenRapidFire -= Time.deltaTime;
        timeBetweenCircleAttack -= Time.deltaTime;
        if (timeBetweenCircleAttack <= 0){
            circleAttack();
            timeBetweenCircleAttack = startTimeBetweenCircleAttack;
        }
        if (timeBetweenRapidFire <= 0){
            StartCoroutine(rapidFire());
            timeBetweenRapidFire = starttimeBetweenRapidFire;
        }
    }
    
    void circleAttack(){
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(var i = 0; i < numberOfProjectiles - 1; i++) {
            float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5f;
            float projectileDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5f;

            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectiveMoveDirection = new Vector2 (projectileVector.x - transform.position.x, projectileVector.y - transform.position.y).normalized;

            var proj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            proj.GetComponent<BulletScript>().setBulletShooter(gameObject.transform.GetChild(0).gameObject);
            proj.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(projectiveMoveDirection.x * 20f, projectiveMoveDirection.y * 20f);
            // proj.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.GetChild(0).up * 20f, ForceMode2D.Impulse);

            angle += angleStep;
        }
    }

    IEnumerator rapidFire(){
        shooting.startTimeBetweenShots = 0.1f;
        yield return new WaitForSeconds(3f);
        shooting.startTimeBetweenShots = 1f;
    }
}
