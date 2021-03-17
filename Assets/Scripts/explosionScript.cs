using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    public float lifeTime;
    public int damage;
    private void Awake() {
        Destroy (gameObject, lifeTime);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other) {
            
         // get the point of contact
         Debug.Log("explosioncollision");
        GameObject colliderObject = other.gameObject;
        if(colliderObject.GetComponent<PlayerHPManager>() != null)
        {
            colliderObject.GetComponent<PlayerHPManager>().takeDamage(damage);
            this.GetComponent<Collider2D>().isTrigger = true;
        }
     }
}
