using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    public float lifeTime;

    GameObject shooter;
    public GameObject hitEffect;
    public bool playerBullet = false;
    
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector2 oldVelocity;

    void FixedUpdate()
    {
        // because we want the velocity after physics, we put this in fixed update
        oldVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // get the point of contact
        GameObject colliderObject = other.gameObject;
        if (colliderObject.tag == "Mirror")
        {
            ContactPoint2D contact = other.GetContact(0);

            // reflect our old velocity off the contact point's normal vector
            Vector2 reflectedVelocity = Vector2.Reflect(oldVelocity, contact.normal);

            // assign the reflected velocity back to the rigidbody
            GetComponent<Rigidbody2D>().velocity = reflectedVelocity;
            // rotate the object by the same ammount we changed its velocity
            Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
            transform.rotation = rotation * transform.rotation;
            damage += 1;
        }
        else if (colliderObject.GetComponent<HitPointManager>() != null)
        {
            var effectInst = Instantiate(hitEffect, 
                transform.position, 
                transform.rotation);
            GameManager.instance.StartCoroutine(Commons.FadeAway(effectInst.GetComponentInChildren<SpriteRenderer>(), 0.12f));
            GameManager.instance.StartCoroutine(Commons.DelayedAction(() => Destroy(effectInst), 0.8f));
            colliderObject.GetComponent<HitPointManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if (colliderObject.GetComponent<PlayerHPManager>() != null)
        {
            colliderObject.GetComponent<PlayerHPManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        //layer 3 is the obstacle layer
        else if (colliderObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }

    public void setBulletShooter(GameObject owner)
    {
        shooter = owner;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shooter.GetComponentInChildren<Collider2D>());
    }
}