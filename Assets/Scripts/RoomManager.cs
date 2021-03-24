using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float roomTime;
    public bool cleared;
    public bool current;
    public int numberOfEnemies;
    public GameObject timerCanvas;
    public bool leftDoorOpens;
    public bool rightDoorOpens;
    public bool topDoorOpens;
    public bool bottomDoorOpens;
    public bool bossFight;
    private TimeManager timeManager;


    private bool closedDoors;
    private GameObject topDoor;
    private GameObject bottomDoor;
    private GameObject leftDoor;
    private GameObject rightDoor;
    private Animator anim;



    
    void Start()
    {
        timeManager = timerCanvas.GetComponent<TimeManager>();
        cleared = false;
        closedDoors = true;
        foreach (Transform child in transform){
            if (child.name == "TopDoor"){
                topDoor = child.gameObject;
            }
            else if (child.name == "BottomDoor"){
                bottomDoor = child.gameObject;
            }
            else if (child.name == "LeftDoor"){
                leftDoor = child.gameObject;
            }
            else if (child.name == "RightDoor"){
                rightDoor = child.gameObject;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies <= 0 && !cleared){
            if (timeManager)
                timeManager.stop = true;
            cleared = true;
        }
        if (cleared && closedDoors){
            if (leftDoor && leftDoorOpens){anim = leftDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); leftDoor.transform.Find("Red").gameObject.SetActive(false); leftDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (rightDoor && rightDoorOpens){anim = rightDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); rightDoor.transform.Find("Red").gameObject.SetActive(false); rightDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (topDoor && topDoorOpens){anim = topDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); topDoor.transform.Find("Red").gameObject.SetActive(false); topDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (bottomDoor && bottomDoorOpens){anim = bottomDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); bottomDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            closedDoors = false;
        }
    }

    public void killedEnemy(){
        if (!bossFight){
            numberOfEnemies -= 1;
        }
    }

    public void setCurrentRoom(){
        current = true;
        if (!cleared){
            timeManager.seconds += roomTime;
            timeManager.stop = false;
            Transform EnemiesObject = transform.Find("Enemies");
            foreach (Transform enemy in EnemiesObject.transform)
            {
                
                if (enemy.name == "SuicideBomber"){
                    enemy.GetComponent<SuicideBomb>().activate();
                }
                else if (enemy.name == "AstarTestEnemy"){
                    enemy.GetComponent<BasicEnemyShooting>().activate();
                }
                else if (enemy.name == "Boss"){
                    enemy.GetComponent<BasicEnemyShooting>().activate();
                    enemy.GetComponent<BossSpawnEnemies>().activate();
                    enemy.GetComponent<BossAbilities>().activate();
                }
                if (enemy.name != "Boss"){
                    enemy.GetComponent<AIPath>().canMove = true;
                    enemy.GetComponent<AIPath>().canSearch = true;
                }
            }
        }
    }
}
