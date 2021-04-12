using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HitPointManager : MonoBehaviour
{
    public int maxHitpoints;
    public int currentHitPoints;
    public GameObject room;
    public GameObject nextLeveLStairs;
    public EnemyHealthBar healthBar;
    public AudioClip hitSound;
    private RoomManager roomManager;
    private AudioSource audioSource;


    void Start(){
        if (!room){room = transform.parent.parent.parent.gameObject;}
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        maxHitpoints = currentHitPoints;
        roomManager = room.GetComponent<RoomManager>();
        healthBar.SetHealth(currentHitPoints, maxHitpoints);
    }

    private void indicateDamage(){
        if (transform.parent.name == "AstarTestEnemy"){
            StartCoroutine(Damage1());
        }
        if (transform.parent.name == "SuicideBomber" || transform.parent.name == "SuicideBomber(Clone)"){
            StartCoroutine(Damage2());
        }
        if (transform.parent.name == "NinjaEnemy" || transform.parent.name == "NinjaEnemy(Clone)"){
            StartCoroutine(Damage3());
        }
        if (transform.parent.name == "Boss"){
            StartCoroutine(DamageBoss1());
        }
        if (transform.parent.name == "Boss2"){
            StartCoroutine(DamageBoss2());
        }
        if (transform.parent.name == "BossNinja"){
            StartCoroutine(DamageBoss1());
        }
    }

    public void takeDamage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        audioSource.PlayOneShot(hitSound);
        indicateDamage();
        if (healthBar) {healthBar.SetHealth(currentHitPoints, maxHitpoints);}
        if (currentHitPoints <= 0)
        {
            var range = Random.Range(0, 100);
            if (range <= 25 && Math.Abs(GameManager.instance.globalTimeMult - 1f) < 0.01f && !SlowMotionPickupScript.exists)
            {
                //print("Bam");
                Instantiate(Resources.Load("Prefabs/Pickup"), transform.position, transform.rotation);
            }
            else
            {
                //print(SlowMotionPickupScript.exists);
                //print(range);
            }
            if (transform.parent != null){
                if (transform.parent.gameObject.name == "Boss" || transform.parent.gameObject.name == "Boss2" && nextLeveLStairs){
                    var stairs = Instantiate(nextLeveLStairs, transform.parent.transform.position, new Quaternion(0, 0, 0, 0));
                    roomManager.killedBoss();
                }

                if (transform.parent.gameObject.name == "BossNinja")
                {
                    roomManager.killedBoss();
                    SceneManager.LoadScene(4);
                }
                roomManager.killedEnemy();
                Destroy(transform.parent.gameObject);
            }
            else{
                roomManager.killedEnemy();
                Destroy(gameObject);
            }

        }
    }

    IEnumerator Damage1(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(17, 231, 0, 255);
    }

    IEnumerator Damage2(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 123, 0, 255);
    }

    IEnumerator Damage3(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    IEnumerator DamageBoss1(){
        transform.parent.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);
        transform.parent.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    IEnumerator DamageBoss2(){
        transform.parent.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 255);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 255);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        transform.parent.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    public void destroyFromParent(){
        roomManager.killedEnemy();
    }
}
