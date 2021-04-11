using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomTime;
    public bool cleared;
    public bool current;
    public int numberOfEnemies = 0;
    public GameObject timerCanvas;
    public TextMeshProUGUI timerVisualIndicator;
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

    public AudioClip bossMusic;

    
    void Start()
    {
        timerCanvas = GameObject.Find("TimerCanvas");
        timeManager = timerCanvas.GetComponent<TimeManager>();
        cleared = false;
        closedDoors = true;
        if (numberOfEnemies == 0){
            foreach (Transform enemy in transform.Find("Enemies")){
                if (enemy.name != "Turret")
                {
                    numberOfEnemies += 1;    
                }
            }
        }
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
            if (bossFight)
            {
                Transform EnemiesObject = transform.Find("Enemies");
                foreach (Transform enemy in EnemiesObject.transform)
                {

                    if (enemy.name == "Turret")
                    {
                        enemy.transform.GetChild(0).GetComponent<BasicEnemyShooting>().canShoot = false;
                    }
                }
            }

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

    public void killedBoss(){
        numberOfEnemies -= 1;
    }

    public void setCurrentRoom(){
        current = true;
        if (!cleared){
            timeManager.seconds += roomTime;
            if (timerVisualIndicator){StartCoroutine(indicateTime());}
            timeManager.stop = false;
            Transform EnemiesObject = transform.Find("Enemies");
            foreach (Transform enemy in EnemiesObject.transform)
            {
                
                if (enemy.name == "SuicideBomber"){
                    enemy.GetComponent<SuicideBomb>().activate();
                }
                else if (enemy.name == "AstarTestEnemy" || enemy.name == "NinjaEnemy"){
                    enemy.GetComponent<BasicEnemyShooting>().activate();
                }
                else if (enemy.name == "Boss" || enemy.name == "BossNinja"){
                    enemy.GetComponent<BasicEnemyShooting>().activate();
                    enemy.GetComponent<BossSpawnEnemies>().activate();
                    enemy.GetComponent<BossAbilities>().activate();
                }
                else if (enemy.name == "Boss2"){
                    enemy.GetComponent<Boss2Shooting>().activate();
                }
                else if (enemy.name == "Turret"){
                    enemy.transform.GetChild(0).GetComponent<BasicEnemyShooting>().activate();
                }
                if (enemy.name != "Boss" && enemy.name != "Boss2" && enemy.name != "Turret"){
                    enemy.GetComponent<AIPath>().canMove = true;
                    enemy.GetComponent<AIPath>().canSearch = true;
                }
            }
        }

        if (name == "BossRoom")
        {
            var audioGO = GameObject.Find("Audio");
            if (audioGO)
            {
                var ctx = audioGO.GetComponent<AudioSource>();
                if (audioGO && bossMusic && ctx.clip != bossMusic)
                {
                    ctx.clip = bossMusic;
                    ctx.Play();
                }
            }
        }
    }
    IEnumerator indicateTime(){
        timerVisualIndicator.color = new Color32(0, 0, 200, 255);
        timerVisualIndicator.text = "+ " + roomTime;
        timerCanvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 200, 255);
        yield return new WaitForSeconds(0.5f);
        timerCanvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        timerVisualIndicator.color = new Color32(255, 255, 255, 255);
        timerVisualIndicator.text = "";
    }
}
