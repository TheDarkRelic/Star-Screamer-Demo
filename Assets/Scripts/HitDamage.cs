using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public int health;
    public GameObject explosionPreFab;

    public Player _player;
    public  bool isDamageable = true;
    private InstaniateExplosion _explosionFX;
    void OnEnable()
    {
        TriggerCollider.OnTriggerAction += ProcessDamage;
    }

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
            StartCoroutine(DamageCoolDown());
            health -= damageAmount;
            if (health < 1)
            {
                _player.DestroyPlayer();
                _explosionFX.InitExplosion(this.gameObject);
            }
        }
    }

    IEnumerator DamageCoolDown()
    {
        isDamageable = false;
        yield return new WaitForSeconds(0.5f);
        isDamageable = true;
    }

    void OnDisable()
    {
        TriggerCollider.OnTriggerAction -= ProcessDamage;
    }
}
