using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollison : MonoBehaviour
{
    [SerializeField] private int _health = 12;
    [SerializeField] GameObject _asteroidBurst = null;
    [SerializeField] GameObject _hitParticles = null;
    [SerializeField] private int _damageAmount = 1;
    private Spawner _spawner = null;

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
            }
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            Instantiate(_hitParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _health--;
            if (_health < 1)
            {
                if (_spawner != null && gameObject.CompareTag("Asteroid"))
                {
                    _spawner.StartCoroutine(_spawner.SpawnPowerupItem(this.transform));
                }

                if (!this.gameObject.CompareTag("Bits"))
                {
                    Instantiate(_asteroidBurst, transform.position, _asteroidBurst.transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
