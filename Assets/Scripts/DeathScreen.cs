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
        GameManager.instance.globalTimeMult = 1;
        SlowMotionPickupScript.exists = false;
    }

    public void Respawn(){
        GameManager.instance.globalTimeMult = 1;
        SlowMotionPickupScript.exists = false;
        SceneManager.LoadScene(level);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
}
