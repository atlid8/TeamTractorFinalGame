using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    public int currentHitPoints;

    public GameObject room;
    private RoomManager roomManager;

    void Start(){
        roomManager = room.GetComponent<RoomManager>();
    }

    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
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
