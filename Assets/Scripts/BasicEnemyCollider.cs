using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCollider : MonoBehaviour
{
    public static Action<int> OnTriggerAction;

    [SerializeField] int _damageAmount;
    [SerializeField] GameObject _hitParticles;
    [HideInInspector] public Player _player;
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerAction?.Invoke(_damageAmount);

            if (this.CompareTag("Enemy"))
            {
                enemy.DestroyEnemy();
            }

            if (this.CompareTag("Bits"))
            {
                Destroy(this.gameObject);
            }

        }
        else if (other.CompareTag("Laser"))
        {
            Instantiate(_hitParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GetComponent<IDamageable>().ProcessDamage(1);
        }
        else if (other.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
            GetComponent<IDamageable>().ProcessDamage(2);
        }
    }
}
