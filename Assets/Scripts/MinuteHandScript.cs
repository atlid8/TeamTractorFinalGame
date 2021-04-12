using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHandScript : MonoBehaviour
{
    private TimeManager timeManager;
    private GameObject timerCanvas;
    private Transform anchor;
    private float secs;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        timerCanvas = GameObject.Find("TimerCanvas");
        timeManager = timerCanvas.GetComponent<TimeManager>();
        anchor = transform.parent;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        secs = timeManager.seconds;
        angle = (secs / 60) * 360;
        transform.eulerAngles = new Vector3(0,0,angle);
    }
}
