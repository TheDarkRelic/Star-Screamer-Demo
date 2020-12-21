using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 500;
    public bool isDamageable;

    void Start()
    {
        isDamageable = false;
    }

    public void ProcessDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Laser"))
            return;
        Destroy(other.gameObject);
        if (isDamageable) ProcessDamage(1);

        if (!other.CompareTag("Missile"))
            return;
        Destroy(other.gameObject);
        if (isDamageable) ProcessDamage(2);
    }


    private void SetDamageable()
    {
        isDamageable = true;
    }

    private void SetNotDamageable()
    {
        isDamageable = false;
    }

    private IEnumerator BossShieldCoolDown()
    {
        isDamageable = false;
        yield return new WaitForSeconds(3);
        isDamageable = true;
    }
}
