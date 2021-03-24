using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairSceneTransition : MonoBehaviour
{
    public int currentScene;
    public int nextScene;
    
    void OnTriggerEnter2D(Collider2D other) {
        // Play some animation?
        if (other.name == "Player"){
            SceneManager.LoadScene(nextScene);
        }
    }
}
