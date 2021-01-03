using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAnimEvent : MonoBehaviour
{
    public static Action onBossDestroy;
    public static Action onBossEntry;
    public ParticleSystem _photonLaserParticles = null;
    [SerializeField] ParticleSystem _photonMissileParticlesLocal = null;
    [SerializeField] ParticleSystem _photonMissileParticlesWorld = null;
    [SerializeField] ParticleSystem _photonShieldParticles = null;
    [SerializeField] ParticleSystem _photonShieldPointParticle = null;
    private CircleCollider2D _shieldCollider = null;
    private CircleCollider2D[] _bossCollider = null;
    [SerializeField] AudioClip _missileSfx = null;
    [SerializeField] float _missileSfxVolume = 0.5f;

    private void Start()
    {
        _shieldCollider = GameObject.Find("Boss1_shield").GetComponent<CircleCollider2D>();
        _bossCollider = GetComponents<CircleCollider2D>();
        onBossEntry?.Invoke();
    }

    private void PlayWarning()
    {
        var warning = GameObject.Find("Warning").GetComponent<Animator>();
        warning.SetTrigger("warning");
    }

    void FireLaserCanon()
    {
        if (Boss.isAlive)
        {
            _photonLaserParticles.Play();
        }
 
    }

    void FirePhotonMissileLocal()
    {
        if (Boss.isAlive)
        {
            StartCoroutine(DoubleMissileSfx());
            _photonMissileParticlesLocal.Play();
        }
        
    }

    void FirePhotonMissileWorld()
    {
        if (Boss.isAlive)
        {
            PhotonMissileSfx();
            _photonMissileParticlesWorld.Play();
        }
        
    }

    private void PhotonMissileSfx() => AudioSource.PlayClipAtPoint(_missileSfx, Camera.main.transform.position, _missileSfxVolume);
    private IEnumerator DoubleMissileSfx()
    {
        if (Boss.isAlive)
        {
            PhotonMissileSfx();
            yield return new WaitForSeconds(.1f);
            PhotonMissileSfx();
        }
        
    }

    void DeactivateShieldCollider()
    {
        _shieldCollider.enabled = false;
    }

    void ActivateBossCollider()
    {
        foreach (var collider in _bossCollider)
        {
            collider.enabled = true;
        }
    }

    void DeactivateBossCollider()
    {
        foreach (var collider in _bossCollider)
        {
            collider.enabled = false;
        }
    }

    void ActivateShieldCollider()
    {
        _shieldCollider.enabled = true;
    }

    void AddRigidBody2D()
    {
        var checkForRB = GetComponent<Rigidbody2D>();
        if (checkForRB == null)
        {
            var rigBod = gameObject.AddComponent<Rigidbody2D>();
            rigBod.gravityScale = 0;
        }
        
    }

    IEnumerator ActivatePhotonShield()
    {
        if (Boss.isAlive)
        {
            _shieldCollider.enabled = true;
            _photonShieldParticles.Play();
            _photonShieldPointParticle.Play();
            yield return new WaitForSeconds(4);
            _shieldCollider.enabled = false;
        }

    }

    private void OnDestroy()
    {
        onBossDestroy?.Invoke();
    }

    private void OnEnable()
    {
        onBossEntry += PlayWarning;
    }

    private void OnDisable()
    {
        onBossEntry -= PlayWarning;
    }
}
