using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[2];
    [SerializeField] GameObject[] powerups;
    [SerializeField] GameObject _enemyCapsule;
    [SerializeField] GameObject _enemyPreFab;
    [SerializeField] float _minSpawnPowerup = 3f;
    [SerializeField] float _maxSpawnPowerup = 7f;
    bool _stopSpawning = false;
    private Enemy _enemy;

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
        StartCoroutine(SpawnShipEnemy());
        StartCoroutine(SpawnMineEnemy());
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnShipEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomSpawnTime = Random.Range(2, 6);
            var x = Random.Range(-2.9f, 2.9f);
            var randomSpawnPos = new Vector2(x, this.transform.position.y);
            GameObject newEnemy = (GameObject)Instantiate(enemies[0], randomSpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyCapsule.transform;
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    IEnumerator SpawnMineEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomSpawnTime = Random.Range(5, 8);
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
            var _powerupSpawnRate = Random.Range(_minSpawnPowerup, _maxSpawnPowerup);
            yield return new WaitForSeconds(_powerupSpawnRate);
            Vector2 positionToSpawn = new Vector2(Random.Range(-2.8f, 2.8f), 7);
            int randomPowerup = Random.Range(0, 4);
            Instantiate(powerups[randomPowerup], positionToSpawn, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
 