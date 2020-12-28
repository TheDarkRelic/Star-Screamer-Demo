using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public static Action<int> OnEnemyDamage;
    public static Action<GameObject> OnEnemyDestroy;
    public int Health { get => _enemyHealth; set => _enemyHealth = value; }
    [SerializeField] private int _enemyHealth = 5;
    [SerializeField] int scoreAmount;
    private InstantiateExplosion _explosionFX;

    private void Awake()
    {
        _explosionFX = GetComponent<InstantiateExplosion>();
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
