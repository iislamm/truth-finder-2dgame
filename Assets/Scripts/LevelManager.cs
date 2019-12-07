using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public GameObject currentCheckpoint;
    public GameObject thugGameObject;
    public int currentLevel = 1;
    public int maxEnemiesCount = 5;
    public int spawnedEnemiesCount = 0;
    public int currentEnemiesCount = 0;
    public int concurrentEnemiesLimit = 2;
    private Vector2 _enemySpawnPoint;
    public GameObject gangHeadGameObject;
    public GameObject bossGameObject;
    private readonly int _bossScene = 2;
    private Scene _currentScene;
    private int _enemySpawnTimer;

    private void Start()
    {
        UpdateEnemySpawnPoint();
        maxEnemiesCount = 5;
        spawnedEnemiesCount = 0;
        currentEnemiesCount = 0;
        concurrentEnemiesLimit = 2;
        _currentScene = SceneManager.GetActiveScene();
        _enemySpawnTimer = 0;
    }

    private void Update()
    {
        _enemySpawnTimer--;
        if (spawnedEnemiesCount < maxEnemiesCount && currentEnemiesCount < concurrentEnemiesLimit && _enemySpawnTimer <= 0)
        {
            SpawnEnemy(thugGameObject);
            _enemySpawnTimer = 50;
        }
    }

    public void RespawnPlayer(){
        FindObjectOfType<PlayerController>().transform.position = currentCheckpoint.transform.position;
    }
    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, _enemySpawnPoint, transform.rotation);
        currentEnemiesCount++;
        spawnedEnemiesCount++;
    }
    public void EnemyDown(GameObject killedEnemy)
    {
        currentEnemiesCount--;
        FindObjectOfType<PlayerStats>().AddPoints(killedEnemy.GetComponent<EnemyController>().pointsValue);
    }

    public void LastEnemyDown()
    {
        Debug.Log(_currentScene.buildIndex);
        FindObjectOfType<NavigationController>().GoToNextScene(_currentScene.buildIndex);
    }
    public void NewCheckPoint(GameObject checkpoint)
    {
        if (currentCheckpoint != checkpoint && currentCheckpoint.transform.position.x < checkpoint.transform.position.x)
        {
            currentCheckpoint = checkpoint;
            maxEnemiesCount += checkpoint.GetComponent<CheckpointController>().enemiesCount;
            if (checkpoint.GetComponent<CheckpointController>().isLast)
            {
                GameObject lastEnemy = _currentScene.buildIndex == _bossScene ? bossGameObject : gangHeadGameObject;
                SpawnEnemy(lastEnemy);
            }
        }
        UpdateEnemySpawnPoint();
    }

    private void UpdateEnemySpawnPoint()
    {
        Vector2 checkpointPosition = currentCheckpoint.transform.position;
        _enemySpawnPoint = new Vector2(checkpointPosition.x + 30, checkpointPosition.y);
    }
}