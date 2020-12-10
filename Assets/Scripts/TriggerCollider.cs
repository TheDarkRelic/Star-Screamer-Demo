using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{

    public static Action<int> OnTriggerAction;

    private int _damageAmount; 

    public Player _player;
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _damageAmount = 1;
            OnTriggerAction?.Invoke(_damageAmount);
            enemy.DestroyEnemy();

        }

        if (other.CompareTag("Laser") || other.CompareTag("Missile"))
        {
            if (other.CompareTag("Missile"))
            {
                Enemy.OnEnemyDamage?.Invoke(2);
            }
            Enemy.OnEnemyDamage?.Invoke(1);
            Destroy(other.gameObject);
        }
    }
}
