using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    public int maxHitpoints;
    public int currentHitPoints;
    public GameObject room;
    public EnemyHealthBar healthBar;
    private RoomManager roomManager;


    void Start(){
        roomManager = room.GetComponent<RoomManager>();
        healthBar.SetHealth(currentHitPoints, maxHitpoints);
    }

    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        healthBar.SetHealth(currentHitPoints, maxHitpoints);
        if (currentHitPoints <= 0)
        {
            if (transform.parent != null){
                roomManager.killedEnemy();
                Destroy(transform.parent.gameObject);
            }
            else{
                roomManager.killedEnemy();
                Destroy(gameObject);
            }
        }
    }

    public void destroyFromParent(){
        roomManager.killedEnemy();
    }
}
