using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEnemies : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject enemyPrefab;
    public GameObject enemyParentComponent;
    private int spawnIndex;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = spawns.Length;
        InvokeRepeating("spawnEnemys", 1, 5);
    }

    void spawnEnemys(){
        spawnIndex = Random.Range(0, count);
         
        GameObject enemy = Instantiate(enemyPrefab, spawns[spawnIndex].position, enemyPrefab.transform.rotation);
        enemy.transform.parent = enemyParentComponent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
