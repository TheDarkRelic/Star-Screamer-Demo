using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public static Action OnPlayerDeathAction;
    public int health;
    public GameObject explosionPreFab;

    public Player _player;
    public  bool isDamageable = true;

    void OnEnable()
    {
        TriggerCollider.OnTriggerAction += ProcessDamage;
    }

    void Awake()
    {
        OnPlayerDeathAction += DestroyPlayer;
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
                if (OnPlayerDeathAction == null) return;
                OnPlayerDeathAction();
            }
        }
    }

    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
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
