using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public static Action<int> OnEnemyDamage;
    public static Action<GameObject> OnEnemyDestroy;

    [SerializeField] private int _enemyHealth = 5;
    [SerializeField] int scoreAmount;
    private InstaniateExplosion _explosionFX;

    private void Awake()
    {
        _explosionFX = GetComponent<InstaniateExplosion>();
    }

    public void ProcessDamage(int damageAmount)
    {
        _enemyHealth -= damageAmount;

        if (_enemyHealth < 1)
        {
            _enemyHealth = 0;
            DestroyEnemy();
        }
    }
    public void DestroyEnemy()
    {
        _explosionFX.InitExplosion(this.gameObject);
        EventsList.OnScoreAction?.Invoke(scoreAmount);
        Destroy(this.gameObject);
    }
}
