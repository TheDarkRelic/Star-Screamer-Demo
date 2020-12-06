using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField] float _speed = 4f;
    private Player _player;
    [SerializeField] GameObject _explosionFxPreFab;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _laserSFX;
    [SerializeField] GameObject _laserPreFab;
    [SerializeField] float _laserOffset;
    private SpriteRenderer _sprite;
    float _canFire = -1.0f;
    [SerializeField] private int _enemyHealth = 5;
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _followsPlayer = true;
    public int enemyID;
    private GameObject explosion;

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Enemy _audioSource is Null");
        }
        _player = GameObject.Find("Player_1").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        _speed = Random.Range(2, 5);
    }

    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire && GetComponent<BoxCollider2D>() != null && _canShoot)
        {
            var _fireRate = Random.Range(0.5f, 3.0f);
            _canFire = Time.time + _fireRate;
            _audioSource.PlayOneShot(_laserSFX, 0.5f);
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
        if (!_followsPlayer)
            transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

        if (transform.position.y <= -5f)
        {
            float randomXpos = Random.Range(-3f, 3f);
            transform.position = new Vector3(randomXpos, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(1);
            }

            _speed = 0;
            if (_sprite != null)
            {
                _sprite.enabled = false;
            }
            explosion = Instantiate(_explosionFxPreFab, transform.position, Quaternion.identity);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Laser") || other.CompareTag("Missile"))
        {
            if (other.CompareTag("Missile"))
            {
                _enemyHealth--;
            }
            _enemyHealth--;
            Destroy(other.gameObject);
            if (_player == null)
                return;

            if (_enemyHealth > 0)
                return;

            if (this.enemyID == 0)
            {
                _player.Score(10);
            }
            else if (this.enemyID == 1)
            {
                _player.Score(30);
            }

            _speed = 0;
            if (_sprite != null)
            {
                _sprite.enabled = false;
            }

            if (_enemyHealth < 1)
            { 
                explosion = Instantiate(_explosionFxPreFab, transform.position, Quaternion.identity);
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 0.5f);
            }
            
        }
    }
}
