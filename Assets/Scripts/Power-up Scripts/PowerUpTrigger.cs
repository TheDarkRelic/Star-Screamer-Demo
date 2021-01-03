using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTrigger : MonoBehaviour, IScoreable
{

    [SerializeField] AudioClip _powerupSfx = null;
    public float sFxVolume = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        var powerUp = GetComponent<IPowerup>();
        if (powerUp != null)
        {
            powerUp.ActivatePowerUp();
            Score(100);
            AudioSource.PlayClipAtPoint(_powerupSfx, Camera.main.transform.position, sFxVolume);
            Destroy(this.gameObject);
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
