using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private float shakeTimer;
    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update(){
        if (shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin t = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                t.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(){
        float intensity = 2f;
        float timer = 0.5f;
        CinemachineBasicMultiChannelPerlin t = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        t.m_AmplitudeGain = intensity;
        shakeTimer = timer;
    }
}
