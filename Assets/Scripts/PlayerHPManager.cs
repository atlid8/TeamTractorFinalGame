using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHPManager : MonoBehaviour
{
    public bool shakeScreen = false;
    public GameObject timerCanvas;
    public TextMeshProUGUI timerDisplay;
    private TimeManager timeManager;
    public GameObject camera;
    private CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = camera.GetComponent<CameraShake>();
        timeManager = timerCanvas.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damageAmount)
    {
        if (timeManager.seconds >= 1 && damageAmount <= 1){
            timeManager.seconds -= damageAmount;
            StartCoroutine(DamageText());
        }
        else {
            timeManager.seconds = 0;
            timeManager.miliseconds = 0;
        }
        if (shakeScreen){
            cameraShake.ShakeCamera();
        }
        if (timeManager.seconds <= 0 && timeManager.miliseconds <= 0)
        {
            // Player is dead
            Destroy(this.gameObject);
        }
    }

    IEnumerator DamageText()
    {
        timerDisplay.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSecondsRealtime(0.25f);
        timerDisplay.color = new Color32(255, 255, 255, 255);
    }

}
