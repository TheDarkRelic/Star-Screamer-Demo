using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsTrigger : MonoBehaviour
{
    [SerializeField] private HitDamage _hitDamage = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            _hitDamage.ProcessDamage(0);
            gameObject.SetActive(false);
        }
    }
}
