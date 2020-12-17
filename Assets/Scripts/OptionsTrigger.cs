using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HitDamage _hitDamage;
    [SerializeField] private GameObject _explosionFx;
    private bool _isActive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            _hitDamage.ProcessDamage(0);
            Instantiate(_explosionFx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
