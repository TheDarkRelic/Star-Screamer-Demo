using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] AudioSource aSource;
    [SerializeField] float _speed = 8.0f;
    public int damageAmount;
    private bool _isEnemyLaser = false;
    private HitDamage _hitDamage = null;
    private Player _player = null;

    void Start()
    {
        _hitDamage = FindObjectOfType<HitDamage>();
        aSource.Play();
    }
    void Update()
    {
        if (_isEnemyLaser == false) MoveUp();
        else MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 6.8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser)
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                if (_hitDamage != null)
                {
                    if (player.shieldActive)
                    {
                        player.shieldActive = false;
                        player.OnShieldDeactivate.Invoke();
                    }
                    else
                    {
                        BasicEnemyCollider.OnTriggerAction?.Invoke(damageAmount);
                    }
                    Destroy(GetComponent<BoxCollider2D>());
                    GetComponentInChildren<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 0.25f);
                }
                
            }
        }
    }
}
