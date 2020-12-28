using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 500;
    public int Health { get => _health; set => _health = value; }

    [SerializeField] GameObject _hitParticles;
    [SerializeField] float _hitParticleOffset;
    [SerializeField] AudioClip _laserSfx;
    [SerializeField] float _laserSfxVolume = 0.5f;
    [SerializeField] float _shieldTimer = 4;
    public bool isDamageable;

    void Start() => isDamageable = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        var x = other.transform.position.x;
        var y = other.transform.position.y + _hitParticleOffset;

        if (!other.CompareTag("Laser"))
            return;
        Instantiate(_hitParticles, new Vector2 (x, y), Quaternion.identity);
        Destroy(other.gameObject);
        if (isDamageable) ProcessDamage(1);

        /*if (!other.CompareTag("Missile"))
            return;
        Destroy(other.gameObject);
        if (isDamageable) ProcessDamage(2);*/
    }

    public void ProcessDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0) Destroy(this.gameObject);
    }

    private void SetDamageable() => isDamageable = true;
    private void SetNotDamageable() => isDamageable = false;
    private IEnumerator BossShieldCoolDown()
    {
        SetNotDamageable();
        yield return new WaitForSeconds(_shieldTimer);
        SetDamageable();
    }

    public void LaserCanonSfx() => AudioSource.PlayClipAtPoint(_laserSfx, Camera.main.transform.position, _laserSfxVolume);

}
