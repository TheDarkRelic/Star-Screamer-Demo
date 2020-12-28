using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvent : MonoBehaviour
{
    [SerializeField] ParticleSystem _photonLaserParticles;
    [SerializeField] ParticleSystem _photonMissileParticlesLocal;
    [SerializeField] ParticleSystem _photonMissileParticlesWorld;
    [SerializeField] ParticleSystem _photonShieldParticles;
    [SerializeField] ParticleSystem _photonShieldPointParticle;
    private CircleCollider2D _shieldCollider;
    private CircleCollider2D[] _bossCollider;
    [SerializeField] AudioClip _missileSfx;
    [SerializeField] float _missileSfxVolume = 0.5f;


    private void Start()
    {
        _shieldCollider = GameObject.Find("Boss1_shield").GetComponent<CircleCollider2D>();
        _bossCollider = GetComponents<CircleCollider2D>();
    }
    void FireLaserCanon()
    {
        _photonLaserParticles.Play();
    }

    void FirePhotonMissileLocal()
    {
        StartCoroutine(DoubleMissileSfx());
        _photonMissileParticlesLocal.Play();
    }

    void FirePhotonMissileWorld()
    {
        PhotonMissileSfx();
        _photonMissileParticlesWorld.Play();
    }

    private void PhotonMissileSfx() => AudioSource.PlayClipAtPoint(_missileSfx, Camera.main.transform.position, _missileSfxVolume);
    private IEnumerator DoubleMissileSfx()
    {
        PhotonMissileSfx();
        yield return new WaitForSeconds(.1f);
        PhotonMissileSfx();
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
        _shieldCollider.enabled = true;
        _photonShieldParticles.Play();
        _photonShieldPointParticle.Play();
        yield return new WaitForSeconds(3);
        _shieldCollider.enabled = false;

    }
}
