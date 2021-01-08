using System;
using System.Collections;
using UnityEngine;

public class HitDamage : MonoBehaviour, IDamageable
{
    public static Action<int> onHitAction;
    public int health;
    public bool isDamageable = true;
    [SerializeField] private float iframeTimer = 1.5f;
    [SerializeField] private SpriteRenderer shipSprite = null;
    [SerializeField] private PlayerShoot playerShoot = null;
    public Player player = null;

    public int Health { get => health; set => health = value; }

    public  void ProcessDamage(int damageAmount)
    {
        if (!isDamageable)
        {
            return;
        }
        if (!player.shieldActive)
        {
            if (health > 0)
            {
                PlayerHit(damageAmount);

                if (health < 1)
                {
                    DestroyPlayer();
                }
            }
            onHitAction(health);
        }
        else
        {
            DeactivateShield();
        }
    }

    private void DeactivateShield()
    {
        player.OnShieldDeactivate?.Invoke();
        player.shieldActive = false;
        StartCoroutine(DamageCoolDown());
    }

    private void DestroyPlayer()
    {
        StopAllCoroutines();
        player.DestroyPlayer();
        var events = FindObjectOfType<EventsList>();
        events.PlayerDeath.Invoke();
    }

    private void PlayerHit(int damageAmount)
    {
        StartCoroutine(DamageCoolDown());
        playerShoot.laserNumber--;
        StartCoroutine(FlashIframes());
        health -= damageAmount;
    }

    private IEnumerator DamageCoolDown()
    {
        SetDamageable(false);
        yield return new WaitForSeconds(iframeTimer);
        SetDamageable(true);
    }

    private IEnumerator FlashIframes()
    {
        while (!isDamageable)
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
