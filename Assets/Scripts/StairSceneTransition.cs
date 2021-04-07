using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairSceneTransition : MonoBehaviour
{
    public int currentScene;
    public int nextScene;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Player" && Math.Abs(GameManager.instance.globalTimeMult - 1) < 0.1f){
            // other.gameObject.GetComponent<PlayerHPManager>().level += 1;
            // GameObject.Find("TimerCanvas").GetComponent<TimeManager>().level += 1;
            GameManager.instance.level += 1;
            SceneManager.LoadScene(nextScene);
        }
    }
}
