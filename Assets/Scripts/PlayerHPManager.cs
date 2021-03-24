using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHPManager : MonoBehaviour
{
    public bool shakeScreen = false;
    public GameObject timerCanvas;
    public TextMeshProUGUI timerDisplay;
    public TextMeshProUGUI timerVisualIndicator;
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
        timeManager.seconds -= damageAmount;
        if (timeManager.seconds >= 1){
            StartCoroutine(DamageText(damageAmount));
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

    IEnumerator DamageText(int damageAmount)
    {
        timerDisplay.color = new Color32(255, 0, 0, 255);
        timerVisualIndicator.color = new Color32(255, 0, 0, 255);
        timerVisualIndicator.text = "- " + damageAmount;
        yield return new WaitForSecondsRealtime(0.5f);
        timerVisualIndicator.text = "";
        timerVisualIndicator.color = new Color32(255, 0, 0, 255);
        timerDisplay.color = new Color32(255, 255, 255, 255);
    }

}
