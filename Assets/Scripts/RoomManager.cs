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
    
    void Start()
    {
        cleared = false;
        timeManager = timerCanvas.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies <= 0){
            Debug.Log("Number of enemies is 0 or less");
            timeManager.stop = true;
            cleared = true;
        }
    }

    public void killedEnemy(){
        numberOfEnemies -= 1;
    }
}
