using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    public bool shakeScreen = false;
    public int currentHitPoints;
    public GameObject camera;
    private CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = camera.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        if (shakeScreen){
            cameraShake.ShakeCamera();
        }
        if (currentHitPoints <= 0)
        {
            if (transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            else{
                Destroy(gameObject);
            }
        }
    }
}
