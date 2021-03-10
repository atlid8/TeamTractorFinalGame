using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerDisplay;
    public float seconds = 10;
    public float miliseconds = 0;   

    private bool stop = false;

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
                }
            }    
        if (!stop){miliseconds -= Time.deltaTime * 100;}
             
        //Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
        timerDisplay.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
        }
    }
}
