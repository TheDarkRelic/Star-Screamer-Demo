using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour, IPowerup
{
    
    public GameObject shieldVisuals;
    [SerializeField] string powerUpName;
    [SerializeField] int powerUpID;

    public string PowerUpName => (powerUpName);

    public int PowerUpID => (powerUpID);

    public void ActivatePowerUp()
    {
        var player = FindObjectOfType<Player>();
        player.shieldActive = true;
        player.OnShieldActivate.Invoke();
    }

}
