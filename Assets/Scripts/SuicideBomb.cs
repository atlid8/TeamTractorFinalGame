using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomb : MonoBehaviour
{
    public float bombTime=5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bombTime <= 0){
            return;
        }
        else{
            bombTime -= Time.deltaTime;
        }
    }
}
