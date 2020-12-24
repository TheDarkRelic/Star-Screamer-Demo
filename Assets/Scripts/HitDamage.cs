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
    [SerializeField] float _iframeTimer;
    public Player _player;
    [SerializeField] SpriteRenderer _shipSrite = null;
    [SerializeField] PlayerShoot _playerShoot;
    private InstaniateExplosion _explosionFX;
    [SerializeField] AudioClip _explosionSFX;
    

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
        if (!_player.shieldActive)
        {
            if (health > 0)
            {
                StartCoroutine(DamageCoolDown());
                _playerShoot.laserNumber--;
                StartCoroutine(FlashIframes());
                health -= damageAmount;
                if (health < 1)
                {
                    StopAllCoroutines();
                    _player.DestroyPlayer();
                    var events = FindObjectOfType<EventsList>();
                    events.PlayerDeath.Invoke();
                }
            }

            OnHitAction(health);
        }
        else
        {
            _player.OnShieldDeactivate?.Invoke();
            _player.shieldActive = false;
            StartCoroutine(DamageCoolDown());
        }
      
    }

    public IEnumerator DamageCoolDown()
    {
        SetDamageable(false);
        yield return new WaitForSeconds(_iframeTimer);
        SetDamageable(true);
    }

    private IEnumerator FlashIframes()
    {
        while(!isDamageable)
        {
            _shipSrite.gameObject.SetActive(false);
            yield return new WaitForSeconds(.04f);
            _shipSrite.gameObject.SetActive(true);
            yield return new WaitForSeconds(.04f);
        }
        
    }
    private void SetDamageable(bool state)
    {
        isDamageable = state;
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
