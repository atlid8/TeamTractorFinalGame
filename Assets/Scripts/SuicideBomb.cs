using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomb : MonoBehaviour
{
    public float bombTime=5.0f;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bombTime <= 0){
            GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
        else{
            bombTime -= Time.deltaTime;
        }
    }
}
