using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI timerDisplay;
    public float seconds = 10;
    public float miliseconds = 0;   

    public bool stop = false;

    void Update(){    
        if (!stop){
            if(miliseconds <= 0){
                miliseconds = 100;
                if(seconds > 0){
                    seconds--;
                }  
                else{
                    stop = true;
                    miliseconds = 0;
                    // Kill character.
                    Application.LoadLevel(0);
                }
            }    
        if (!stop){miliseconds -= Time.deltaTime * 100;}
             
        //Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
        timerDisplay.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
     if(Input.GetKeyDown(KeyCode.R))
         Application.LoadLevel(0); //or whatever number your scene is
 
        }
    }
}
