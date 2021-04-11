using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGameManagerVolume : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
        _slider.value = GameManager.instance.gameVolume;
        _audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    void Update()
    {
        GameManager.instance.gameVolume = _slider.value;
        _audioSource.volume = _slider.value;
    }
}
