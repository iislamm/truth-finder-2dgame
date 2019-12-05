using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour {
    [FormerlySerializedAs("CurrentCheckpoint")] public GameObject currentCheckpoint;
    public Transform enemy;
    public int currentLevel = 1;
    public int maxEnemiesCount = 5;
    public int spawnedEnemiesCount = 0;
    public int currentEnemiesCount = 0;
    public int concurrentEnemiesLimit = 2;
    private Vector2 _enemySpawnPoint;
    public GameObject gangHead;

    private void Start()
    {
        Vector2 checkpointPosition = currentCheckpoint.transform.position;
        _enemySpawnPoint = new Vector2(checkpointPosition.x + 30, checkpointPosition.y);
        maxEnemiesCount = 5;
        spawnedEnemiesCount = 0;
        currentEnemiesCount = 0;
        concurrentEnemiesLimit = 2;
    }

    private void Update()
    {
        if (spawnedEnemiesCount < maxEnemiesCount && currentEnemiesCount < concurrentEnemiesLimit)
        {
            SpawnEnemy();
        }
    }

    public void RespawnPlayer(){
        FindObjectOfType<PlayerController>().transform.position = currentCheckpoint.transform.position;
    }
    private void SpawnEnemy()
    {
        Instantiate(enemy, _enemySpawnPoint, transform.rotation);
        currentEnemiesCount++;
        spawnedEnemiesCount++;
    }
    public void ThugDown(GameObject killedEnemy)
    {
        currentEnemiesCount--;
        FindObjectOfType<PlayerStats>().AddPoints(killedEnemy.GetComponent<ThugController>().GetPointsValue());
    }

    public void gangHeadDown()
    {
        
    }

    private void SpawnGangHead()
    {
        Instantiate(gangHead, _enemySpawnPoint, transform.rotation);
    }

    public void NewCheckPoint(GameObject checkpoint)
    {
        if (currentCheckpoint != checkpoint)
        {
            currentCheckpoint = checkpoint;
            maxEnemiesCount += checkpoint.GetComponent<CheckpointController>().enemiesCount;
            if (checkpoint.GetComponent<CheckpointController>().isLast)
            {
                SpawnGangHead();
            }
        }
    }
    
}