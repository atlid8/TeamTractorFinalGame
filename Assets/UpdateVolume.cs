using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVolume : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = GameManager.instance.gameVolume;
    }
    
}
