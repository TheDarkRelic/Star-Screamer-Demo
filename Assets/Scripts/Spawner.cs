using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[2];
    public GameObject[] spawnPoints = new GameObject[3];
    [SerializeField] GameObject[] _powerups;
    [SerializeField] GameObject _enemyCapsule;
    [SerializeField] GameObject _enemyAnimCapsule;
    [SerializeField] float _minSpawnPowerup = 3f;
    [SerializeField] float _maxSpawnPowerup = 7f;
    [SerializeField] float _enemyAnimeWaitTime = 5f;
    private bool _stopSpawning = false;
    private Enemy _enemy;
    public bool spawnAsteroids;
    public bool spawnEnemyMines;
    public bool spawnEnemyAnims;
    public bool spawnPowerUps;
    [SerializeField] private int _secondTilBossSpawn = 60;
    [SerializeField] private int _spawnAmount = 3;

    void Awake()
    {
        _enemy = FindObjectOfType<Enemy>();
    }
    void Start()
    {
        StartSpawning();
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnBoss());
        if(spawnAsteroids) StartCoroutine(SpawnAsteroid());
        if (spawnEnemyMines) StartCoroutine(SpawnMineEnemy());
        if (spawnPowerUps) StartCoroutine(SpawnPowerup());
        if (spawnEnemyAnims) StartCoroutine(SpawnShipEnemyAnim());
    }

    IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomSpawnTime = Random.Range(6, 8);
            var x = Random.Range(-2.9f, 2.9f);
            var randomSpawnPos = new Vector2(x, this.transform.position.y);
            GameObject newEnemy = (GameObject)Instantiate(enemies[0], randomSpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyCapsule.transform;
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    IEnumerator SpawnShipEnemyAnim()
    {
        yield return new WaitForSeconds(4);
        while (_stopSpawning == false)
        {
            var randomAnim = Random.Range(2, 4);
            for (var i = 0; i < _spawnAmount; i++)
            {
                var randomParent = Random.Range(0, spawnPoints.Length);
                GameObject newEnemy = (GameObject)Instantiate(enemies[randomAnim], transform.position, Quaternion.identity);
                newEnemy.transform.parent =spawnPoints[randomParent].transform;
                yield return new WaitForSeconds(1.2f);
            }
            
            yield return new WaitForSeconds(_enemyAnimeWaitTime);
        }
    }

    IEnumerator SpawnMineEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomSpawnTime = Random.Range(4, 6);
            Vector2 randomSpawnPos = new Vector2(Random.Range(-3.2f, 3.2f), 7);
            GameObject newEnemy = Instantiate(enemies[1], randomSpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyCapsule.transform;
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (_stopSpawning == false)
        {
            var powerupSpawnRate = Random.Range(_minSpawnPowerup, _maxSpawnPowerup);
            yield return new WaitForSeconds(powerupSpawnRate);
            Vector2 positionToSpawn = new Vector2(Random.Range(-2.8f, 2.8f), 7);
            int randomPowerup = Random.Range(0, _powerups.Length);
            Instantiate(_powerups[randomPowerup], positionToSpawn, Quaternion.identity);
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(_secondTilBossSpawn);
        Instantiate(enemies[4], new Vector2(0, 10), Quaternion.identity);
        StopAllCoroutines();
        StartCoroutine(SpawnPowerup());
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
 