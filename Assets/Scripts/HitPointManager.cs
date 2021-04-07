using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitPointManager : MonoBehaviour
{
    public int maxHitpoints;
    public int currentHitPoints;
    public GameObject room;
    public GameObject nextLeveLStairs;
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
            var range = Random.Range(0, 100);
            if (range <= 10 && Math.Abs(GameManager.instance.globalTimeMult - 1f) < 0.01f)
            {
                Instantiate(Resources.Load("Prefabs/Pickup"), transform.position, transform.rotation);
            }
            if (transform.parent != null){
                if (transform.parent.gameObject.name == "Boss" || transform.parent.gameObject.name == "Boss2" && nextLeveLStairs){
                    var stairs = Instantiate(nextLeveLStairs, transform.parent.transform.position, new Quaternion(0, 0, 0, 0));
                    roomManager.killedBoss();
                }
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
