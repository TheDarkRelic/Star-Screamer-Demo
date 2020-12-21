using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollison : MonoBehaviour
{
    [SerializeField] private int _health = 12;
    [SerializeField] GameObject _asteroidBurst;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var iDamage = other.GetComponent<IDamageable>();
            if (iDamage != null)
            {
                print("Boom");
                iDamage.ProcessDamage(3);
            }
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _health--;
            if (_health < 1)
            {
                Instantiate(_asteroidBurst, transform.position, _asteroidBurst.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
