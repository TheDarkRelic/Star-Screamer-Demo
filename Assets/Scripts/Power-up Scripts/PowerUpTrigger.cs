using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTrigger : MonoBehaviour, IScoreable
{

    [SerializeField] AudioSource aSource;
    [SerializeField] AudioClip clip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        var powerUp = GetComponent<IPowerup>();
        if (powerUp != null)
        {
            powerUp.ActivatePowerUp();
            Score(100);
            aSource.Play();
            var sprite = GetComponentInChildren<SpriteRenderer>();
            var collider = GetComponent<CircleCollider2D>();
            sprite.gameObject.SetActive(false);
            collider.enabled = false;
            Destroy(this.gameObject, 1);
        }

        if (other.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
        }
    }

    public void Score(int score)
    {
        EventsList.OnScoreAction?.Invoke(score);
    }
}
