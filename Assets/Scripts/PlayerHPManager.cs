using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int level;
    public AudioSource audioSource;
    public AudioClip hitSound;
    
    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.instance.level;
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
        audioSource.PlayOneShot(hitSound);
        if (timeManager.seconds >= 1){
            StartCoroutine(DamageText(damageAmount));
            StartCoroutine(indicateDamage());
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
            // Destroy(this.gameObject);
            Cursor.visible = true;
            PlayerPrefs.SetInt("level", level);
            SceneManager.LoadScene(5);
        }
    }

    IEnumerator indicateDamage(){
        // Arms
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        // Shotgun
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        // Legs
        transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        // Body
        transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        // Head
        transform.GetChild(1).GetChild(3).GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        transform.GetChild(1).GetChild(3).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    IEnumerator DamageText(int damageAmount)
    {
        
        if (damageAmount > 0){
            timerDisplay.color = new Color32(255, 0, 0, 255);
            timerVisualIndicator.color = new Color32(255, 0, 0, 255);
            timerVisualIndicator.text = "- " + damageAmount;
        }
        else{
            timerDisplay.color = new Color32(127, 255, 0, 255);
            timerVisualIndicator.color = new Color32(127, 255, 0, 255);
            timerVisualIndicator.text = "+ " + damageAmount;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        timerVisualIndicator.text = "";
        timerVisualIndicator.color = new Color32(255, 0, 0, 255);
        timerDisplay.color = new Color32(255, 255, 255, 255);
    }

}
