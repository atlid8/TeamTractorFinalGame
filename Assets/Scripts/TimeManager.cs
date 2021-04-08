using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI timerDisplay;
    public float seconds = 10;
    public float miliseconds = 0;   
    public bool stop;
    public int level;
    private Animator anim;
    

    void Start() {
        level = GameManager.instance.level;    
        stop = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

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
                    Cursor.visible = true;
                    PlayerPrefs.SetInt("level", level);
                    SceneManager.LoadScene(5);
                }
            }    
        if (!stop){miliseconds -= (Time.deltaTime * 100) * GameManager.instance.globalTimeMult;}
             
        //Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
        timerDisplay.text = string.Format("{0}:{1}", seconds, (int)miliseconds);

        // if(Input.GetKeyDown(KeyCode.R))
        //     SceneManager.LoadScene(0); //or whatever number your scene is
        }

        if (seconds < 5)
        {
            timerDisplay.color = Color.red;
            anim.SetBool("isLow", true);
        }
        else
        {
            timerDisplay.color = Color.white;
            anim.SetBool("isLow", false);
        }

    }
    
}
