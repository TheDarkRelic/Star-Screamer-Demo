using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
        }
    }
}
