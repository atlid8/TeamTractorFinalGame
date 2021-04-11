using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningScreen : MonoBehaviour
{
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        GameManager.instance.globalTimeMult = 1;
        SlowMotionPickupScript.exists = false;
    }
    
    public void MainMenu()
    {
        GameManager.instance.level = 1;
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(0);
    }
}
