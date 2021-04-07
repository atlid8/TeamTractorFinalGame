using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossSpawnEnemies : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject enemyPrefab;
    public GameObject enemyParentComponent;
    private int spawnIndex;
    private int count;
    // Start is called before the first frame update
    public void activate()
    {
        count = spawns.Length;
        InvokeRepeating("spawnEnemys", 1, 5);
    }

    void spawnEnemys(){
        spawnIndex = Random.Range(0, count);
         
        GameObject enemy = Instantiate(enemyPrefab, spawns[spawnIndex].position, enemyPrefab.transform.rotation);
        enemy.GetComponent<AIPath>().maxSpeed *= GameManager.instance.globalTimeMult;
        enemy.transform.parent = enemyParentComponent.transform;
        if (enemy.name == "SuicideBomber(Clone)"){
            enemy.GetComponent<SuicideBomb>().activate();
            }
        else if (enemy.name == "AstarTestEnemy(Clone)" || enemy.name == "NinjaEnemy(Clone)"){
            enemy.GetComponent<BasicEnemyShooting>().activate();
            }
        enemy.GetComponent<AIPath>().canMove = true;
        enemy.GetComponent<AIPath>().canSearch = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
