using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    
    public static Action<int> OnTriggerAction;

    private int _damageAmount; 

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
                _damageAmount = 1;
                OnTriggerAction?.Invoke(_damageAmount);
            }
            enemy.DestroyEnemy();
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
