using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] int _playerLives = 3;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _speedBoost = 5f;
    [SerializeField] float _speedCap = 15f;
    [SerializeField] float laserOffset = .08f;
    [SerializeField] float _fireRate = .05f;
    [SerializeField] int _powerUpTimer = 5;
    public int _score = 0; //check to see if this needs to be public
    float _canFire = -1f;
    bool _isDamagable = true;

    public GameObject _laserPreFab;
    public GameObject _tripleShotPreFab;
    public GameObject _shieldVisuals;
    public GameObject _missileOptionsPreFab;

    [SerializeField] GameObject _explosionPreFab;
    [SerializeField] GameObject _leftEngine, _rightEngine;
    [SerializeField] AudioClip _laserSFX;

    Spawner _spawner;
    UIHandler _uiHandler;
    GameHandler _gameHandler;
    AudioSource _audioSource;

    bool _isTripleShotActive = false;
    bool _isShieldActive = false;
    public bool _isPlayer1 = false;
    public bool _isPlayer2 = false;
    public static bool _optionsActive;



    void Awake()
    {
        _optionsActive = false;
        _audioSource = GetComponent<AudioSource>();
        _uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
        _spawner = GameObject.Find("SpawnManager").GetComponent<Spawner>();
        _gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        if (_gameHandler._isSinglePlayer == false)
        {
            if (gameObject.name == "Player_1")
            {
                transform.position = new Vector3(-6.25f, -2.3f, 0);
            }
            else
            {
                transform.position = new Vector3(6.25f, -2.3f, 0);
            }
        }
        else
        {
            transform.position = new Vector3(0, -2.3f, 0);
        }


        if (_spawner == null)
        {
            Debug.LogError("Spawner is Null");
        }

        if (_uiHandler == null)
        {
            Debug.LogError("UIHandler is Null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Player is Null");
        }
        else
        { 
            _audioSource.clip = _laserSFX;
        }

        if (_gameHandler == null)
        {
            Debug.LogError("GameHandler is Null");
        }

    }


    void Update()
    {
        CalculateMovement();

        Player1Shoot();

        Player2Shoot();
    }

    private void Player2Shoot()
    {
        if (_isPlayer2 == true)
        {
            if (Input.GetKey(KeyCode.KeypadEnter) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
    }

    private void Player1Shoot()
    {
        if (_isPlayer1 == true)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPreFab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPreFab, transform.position + new Vector3(0, laserOffset, 0), Quaternion.identity);
        }

        _audioSource.PlayOneShot(_laserSFX, 0.5f);
    }

    private void CalculateMovement()
    {
        if (_isPlayer1 == true)
        {
            float horizontalInput = Input.GetAxis("P1Horizontal");
            float verticalInput = Input.GetAxis("P1Vertical");

            var direction = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        if(_isPlayer2 == true)
        {
            float horizontalInput = Input.GetAxis("P2Horizontal");
            float verticalInput = Input.GetAxis("P2Vertical");

            var direction = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        PlayerBounds();
    }

    private void PlayerBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.3f, 5f), 0);

        if (transform.position.x > 2.8f)
        {
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }
        else if (transform.position.x < -2.8f)
        {
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }
    }

    public void Damage(int damage)
    {
        if (_isDamagable == true)
        {
            if (_isShieldActive == true)
            {
                StartCoroutine(DamageCoolDown());
                _isShieldActive = false;
                _shieldVisuals.SetActive(false);
                return;
            }
     
            else 
            {
                StartCoroutine(DamageCoolDown());
                _playerLives -= damage;

                if(_playerLives == 2)
                {
                    _rightEngine.SetActive(true);
                }
                else if (_playerLives == 1)
                {
                    _leftEngine.SetActive(true);
                }

                _uiHandler.UpdateLives(_playerLives);
            }

            if (_playerLives < 1)
            {

                _spawner.OnPlayerDeath();
                Instantiate(_explosionPreFab, transform.position, Quaternion.identity);
                _uiHandler.CheckForHighScore(_score);
                Destroy(this.gameObject);
            }

        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(_powerUpTimer);
        _isTripleShotActive = false;
    }

    public void SpeedBoost()
    {
        if (_speed >= _speedCap)
        {
            _speed = _speedCap;
        }
        _speed += _speedBoost;
    }

    public void ShieldBoost()
    {
        _isShieldActive = true;
        _shieldVisuals.SetActive(true);
    }

    public void Score(int points)
    {
        _score += points;
        _uiHandler.UpdateScore(_score);
        
    }

    IEnumerator DamageCoolDown()
    {
        _isDamagable = false;
        yield return new WaitForSeconds(0.5f);
        _isDamagable = true;
    }

    public void ActivateMissileOptions()
    {
        if (_optionsActive)
            return;
        
        _optionsActive = true; 
        var options = (GameObject)Instantiate(_missileOptionsPreFab, transform.position, Quaternion.identity);
        options.transform.parent = this.gameObject.transform;
    }
}

