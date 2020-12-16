using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTrigger : MonoBehaviour, IScoreable
{

    SpeedPowerUp _speedPu;
    OptionsPowerUp _optionsPU;
    ShieldPowerUp _shieldPU;
    [SerializeField] AudioClip _powerupSfx;
    public float sFxVolume = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var powerUp = GetComponent<IPowerup>();
            if (powerUp != null)
            {
                Score(100);
                powerUp.ActivatePowerUp();
                AudioSource.PlayClipAtPoint(_powerupSfx, Camera.main.transform.position, sFxVolume);
                Destroy(this.gameObject);
            }

            if (other.CompareTag("EnemyLaser"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void Score(int score)
    {
        EventsList.OnScoreAction?.Invoke(score);
    }
}
