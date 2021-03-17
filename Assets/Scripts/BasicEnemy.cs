using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bullet;
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        var direction = player.position - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(Vector2.Distance(transform.position, player.position) > stoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance){
            transform.position = this.transform.position;
        } else if (Vector2.Distance(transform.position, player.position) < retreatDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBetweenShots <= 0) {
            GameObject shot = Instantiate(bullet, this.transform.position, this.transform.rotation);
            shot.GetComponent<BulletScript>().setBulletShooter(gameObject);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(this.transform.up * 20f, ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;

        }else {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
