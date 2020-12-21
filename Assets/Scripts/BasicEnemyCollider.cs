using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCollider : MonoBehaviour
{
    public static Action<int> OnTriggerAction;

    [SerializeField] int _damageAmount;

    [HideInInspector] public Player _player;
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player.shieldActive)
            {
                player.shieldActive = false;
                player.OnShieldDeactivate.Invoke();
            }
            else
            {
                OnTriggerAction?.Invoke(_damageAmount);
            }

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
