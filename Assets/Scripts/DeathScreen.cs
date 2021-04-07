using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level");
    }

    public void Respawn(){
        SceneManager.LoadScene(level);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
}
