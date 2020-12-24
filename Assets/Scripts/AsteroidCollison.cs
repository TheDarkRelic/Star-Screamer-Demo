using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollison : MonoBehaviour
{
    [SerializeField] private int _health = 12;
    [SerializeField] GameObject _asteroidBurst = null;
    [SerializeField] GameObject _hitParticles = null;
    [SerializeField] GameObject _asteroidParticles = null;
    [SerializeField] private int _damageAmount = 1;
    private Spawner _spawner = null;
    [SerializeField] GameObject _asteroidSFX = null;

    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var iDamage = other.GetComponent<IDamageable>();
            if (iDamage != null)
            {
                iDamage.ProcessDamage(_damageAmount);
                if (!gameObject.CompareTag("Bits"))
                {
                    PlayExplosionSound();
                    InstantiateAsteroidParticles();
                }
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            InstantiateHitParticles(other);
            Destroy(other.gameObject);
            _health--;
            if (_health < 1)
            {
                if (_spawner != null && gameObject.CompareTag("Asteroid"))
                {
                    SpawnRandomPowerup();
                }

                if (!this.gameObject.CompareTag("Bits"))
                {
                    PlayExplosionSound();
                    InstantiateAsteroidParticles();
                    InstantiatAsteroidBurst();
                }
                Destroy(gameObject);
            }
        }
    }

    private void SpawnRandomPowerup()
    {
        _spawner.StartCoroutine(_spawner.SpawnPowerupItem(this.transform));
    }

    private void InstantiateHitParticles(Collider2D other)
    {
        Instantiate(_hitParticles, other.transform.position, Quaternion.identity);
    }

    private void InstantiatAsteroidBurst()
    {
        Instantiate(_asteroidBurst, transform.position, _asteroidBurst.transform.rotation);
    }

    private void InstantiateAsteroidParticles()
    {
        Instantiate(_asteroidParticles, transform.position, Quaternion.identity);
    }

    private void PlayExplosionSound()
    {
        Instantiate(_asteroidSFX, Camera.main.transform.position, Quaternion.identity);
    }
}
