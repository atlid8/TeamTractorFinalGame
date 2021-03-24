using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairSceneTransition : MonoBehaviour
{
    public int currentScene;
    public int nextScene;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hello");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Player"){
            SceneManager.LoadScene(nextScene);
        }
    }
}