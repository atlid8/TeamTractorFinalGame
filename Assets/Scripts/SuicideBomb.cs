using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SuicideBomb : MonoBehaviour
{
    public bool countdownStarted=false;
    public float bombTime=5.0f;
    public GameObject explosionPrefab;

    private float maxSpeed;
    private float changeSpeedTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = this.GetComponent<AIPath>().maxSpeed;
        this.GetComponent<AIPath>().maxSpeed = Random.Range(maxSpeed - 2f, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (bombTime <= 0){
            GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            transform.GetChild(0).GetComponent<HitPointManager>().destroyFromParent();
            Destroy(gameObject);
            
        }
        else if (countdownStarted){
            bombTime -= Time.deltaTime * GameManager.instance.globalTimeMult;
            if (changeSpeedTime <= 0 && GameManager.instance.globalTimeMult >= 0.95f){
                this.GetComponent<AIPath>().maxSpeed = Random.Range(maxSpeed - 5f, maxSpeed);
                changeSpeedTime = 0.5f;
            }
            else{
                changeSpeedTime -= Time.deltaTime;
            }
        }

    }

    public void activate(){
        countdownStarted = true;
    }
}
