using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _explosionFX;
    private Player _player;
    [SerializeField] private GameObject _missilePreFab;

    [SerializeField] float _fireRate = 2.0f;
    [SerializeField] private float _delay = 1f;
    private GameObject _missile;
    private bool _isActive;
    private float _counter = 1;
    void Awake()
    {
        _isActive = true;
        _player = FindObjectOfType<Player>();
    }

    void Start()
    {
        StartCoroutine(SpawnMissilesDelay(_delay));
        StartCoroutine(IframeCounter(_counter));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.CompareTag("Powerup") && !other.CompareTag("Player") && !other.CompareTag("Missile") && !other.CompareTag("Laser"))
        {
            if (!_player)
            {
                _player.Damage(0);
            }

            if (!_isActive)
            {
                Instantiate(_explosionFX, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator SpawnMissiles(float fireRate)
    {
        while (true)
        {   
            _missile = Instantiate(_missilePreFab, transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator SpawnMissilesDelay(float delay)
    {
        yield return  new WaitForSeconds(delay);
        StartCoroutine(SpawnMissiles(_fireRate));
    }

    private IEnumerator IframeCounter(float counter)
    {
        yield return new WaitForSeconds(counter);
        _isActive = false;
    }
}
