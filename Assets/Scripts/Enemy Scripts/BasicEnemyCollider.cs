using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyCollider : MonoBehaviour
{
    public static Action<int> OnTriggerAction;
    [SerializeField] private int damageAmount;
    [SerializeField] private GameObject hitParticles = null;
    public Enemy enemy = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerAction?.Invoke(damageAmount);

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
            var laser = other.gameObject.GetComponent<Laser>();
            Instantiate(hitParticles, other.transform.position, Quaternion.identity);
            GetComponent<IDamageable>().ProcessDamage(laser.damageAmount);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
            GetComponent<IDamageable>().ProcessDamage(2);
        }
    }
}
