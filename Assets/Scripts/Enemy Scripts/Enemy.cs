using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    public static Action OnEnemyDeath;
    public static Action<int> OnEnemyDamage;

    [SerializeField] GameObject _explosionFxPreFab;
    [SerializeField] AudioClip _laserSfx;
    [SerializeField] GameObject _laserPreFab;
    [SerializeField] float _laserOffset;
    [SerializeField] private int _enemyHealth = 5;
    [SerializeField] private bool _canShoot = true;
    public bool followsTarget;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSeed;
    Transform player;

    public float speed = 4f;
    float _canFire = -1.0f;
    private GameObject _explosion;
    public int enemyId;
    public HitDamage hitDamage;
    public AudioSource audioSource;

    void OnEnable()
    {
        OnEnemyDamage += EnemyDamage;
    }
    private void Awake()
    {
        followsTarget = false;
        speed = Random.Range(_minSpeed, _maxSeed);
    }

    void Update()
    {
        CalculateMovement();
        Shoot();
        if (_enemyHealth < 1)
        {
            _enemyHealth = 0;
            DestroyEnemy();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire && GetComponent<BoxCollider2D>() != null && _canShoot)
        {
            var fireRate = Random.Range(0.5f, 3.0f);
            _canFire = Time.time + fireRate;
            audioSource.PlayOneShot(_laserSfx, 0.5f);
            GameObject enemyLaser = Instantiate(_laserPreFab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void CalculateMovement()
    {
        if (!followsTarget)
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        if (transform.position.y <= -5f)
        {
            float randomXpos = Random.Range(-3f, 3f);
            transform.position = new Vector3(randomXpos, 7, 0);
        }
    }


    void DestroyEnemy()
    {
        InstantiateExplosion();
        Destroy(this.gameObject);
    }

    private void InstantiateExplosion()
    {
        _explosion = (GameObject) Instantiate(_explosionFxPreFab, transform.position, Quaternion.identity);
    }

    void EnemyDamage(int damageAmount)
    {
        _enemyHealth -= damageAmount;
    }

    void OnDisable()
    {
        OnEnemyDeath -= DestroyEnemy;
        OnEnemyDamage -= EnemyDamage;
    }


}
