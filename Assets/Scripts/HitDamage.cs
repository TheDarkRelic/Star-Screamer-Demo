using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitDamage : MonoBehaviour, IDamageable
{
    public static Action<int> OnHitAction;
    public int health;
    public bool isDamageable = true;

    public Player _player;
    [SerializeField] PlayerShoot _playerShoot;
    private InstaniateExplosion _explosionFX;
    

    void Start()
    {
        _explosionFX = GetComponent<InstaniateExplosion>();
        
    }

    public void ProcessDamage(int damageAmount)
    {
        if (!isDamageable)
        {
            return;
        }
        
        if (health > 0) 
        {
            _player.OnShieldDeactivate.Invoke();
            StartCoroutine(DamageCoolDown());
            _playerShoot.laserNumber--;
            health -= damageAmount;
            if (health < 1)
            {
                _player.DestroyPlayer();
                _explosionFX.InitExplosion(this.gameObject);
            }
        }

        OnHitAction(health);
    }

    public IEnumerator DamageCoolDown()
    {
        isDamageable = false;

        yield return new WaitForSeconds(1f);
        isDamageable = true;
    }

    void OnEnable()
    {
        BasicEnemyCollider.OnTriggerAction += ProcessDamage;
    }

    void OnDisable()
    {
        BasicEnemyCollider.OnTriggerAction -= ProcessDamage;
    }
}
