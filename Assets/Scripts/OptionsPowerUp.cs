using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _explosionFX;
    private Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.CompareTag("Powerup") && !other.CompareTag("Player"))
        {
            if (!_player)
            {
                _player.Damage(0);
            }
            Instantiate(_explosionFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
