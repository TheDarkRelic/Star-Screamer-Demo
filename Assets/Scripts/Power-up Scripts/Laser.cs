using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] float _speed = 8f;
    public float laserVolume = 0.2f;
    bool _isEnemyLaser = false;
    private HitDamage _hitDamage;

    void Start()
    {
        _hitDamage = FindObjectOfType<HitDamage>();
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, laserVolume);
    }
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
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
            Destroy(this.gameObject);
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
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.8f);
                if (_hitDamage != null)
                {
                    _hitDamage.ProcessDamage(1);
                    Destroy(GetComponent<BoxCollider2D>());
                    GetComponentInChildren<SpriteRenderer>().enabled = false;
                    Destroy(this.gameObject, 0.25f);
                }
                
            }
        }
    }
}
