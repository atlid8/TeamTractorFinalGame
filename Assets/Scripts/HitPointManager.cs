using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    public int currentHitPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
