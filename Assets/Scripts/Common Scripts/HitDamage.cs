using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HitDamage : MonoBehaviour, IDamageable
{
    public static Action<int> onHitAction;
    public int health;
    public bool isDamageable = true;
    [SerializeField] private float iframeTimer;
    [SerializeField] private SpriteRenderer shipSprite = null;
    [SerializeField] private PlayerShoot playerShoot;
    public Player player;

    public int Health { get => health; set => health = value; }

    public void ProcessDamage(int damageAmount)
    {
        if (!isDamageable)
        {
            return;
        }
        if (!player.shieldActive)
        {
            if (health > 0)
            {
                StartCoroutine(DamageCoolDown());
                playerShoot.laserNumber--;
                StartCoroutine(FlashIframes());
                health -= damageAmount;
                if (health < 1)
                {
                    StopAllCoroutines();
                    player.DestroyPlayer();
                    var events = FindObjectOfType<EventsList>();
                    events.PlayerDeath.Invoke();
                }
            }

            onHitAction(health);
        }
        else
        {
            player.OnShieldDeactivate?.Invoke();
            player.shieldActive = false;
            StartCoroutine(DamageCoolDown());
        }
      
    }

    private IEnumerator DamageCoolDown()
    {
        SetDamageable(false);
        yield return new WaitForSeconds(iframeTimer);
        SetDamageable(true);
    }

    private IEnumerator FlashIframes()
    {
        while(!isDamageable)
        {
            shipSprite.gameObject.SetActive(false);
            yield return new WaitForSeconds(.04f);
            shipSprite.gameObject.SetActive(true);
            yield return new WaitForSeconds(.04f);
        }
        
    }
    private void SetDamageable(bool state)
    {
        isDamageable = state;
    }

    private void OnEnable()
    {
        BasicEnemyCollider.OnTriggerAction += ProcessDamage;
    }

    private void OnDisable()
    {
        BasicEnemyCollider.OnTriggerAction -= ProcessDamage;
    }
}
