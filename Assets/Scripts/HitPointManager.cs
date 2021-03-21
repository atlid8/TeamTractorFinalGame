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
        if (!room){room = transform.parent.parent.parent.gameObject;}
        maxHitpoints = currentHitPoints;
        roomManager = room.GetComponent<RoomManager>();
        healthBar.SetHealth(currentHitPoints, maxHitpoints);
    }

    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        if (healthBar) {healthBar.SetHealth(currentHitPoints, maxHitpoints);}
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
