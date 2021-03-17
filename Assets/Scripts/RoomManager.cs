using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool cleared;
    public int numberOfEnemies;
    public GameObject timerCanvas;
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
            if (leftDoor){anim = leftDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); leftDoor.transform.Find("Red").gameObject.SetActive(false); leftDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (rightDoor){anim = rightDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); rightDoor.transform.Find("Red").gameObject.SetActive(false); rightDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (topDoor){anim = topDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); topDoor.transform.Find("Red").gameObject.SetActive(false); topDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            if (bottomDoor){anim = bottomDoor.GetComponent<Animator>(); anim.SetBool("openDoors", true); bottomDoor.transform.Find("ClosedDoorCollider").gameObject.SetActive(false);}
            closedDoors = false;
        }
    }

    public void killedEnemy(){
        numberOfEnemies -= 1;
    }
}
