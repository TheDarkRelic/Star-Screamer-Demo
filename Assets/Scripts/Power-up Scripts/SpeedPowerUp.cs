using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IPowerup
{
    public static Action OnSpeedAction;
   
    [SerializeField] string powerUpName;
    [SerializeField] int powerUpID;

    public string PowerUpName => (powerUpName);
    public int PowerUpID => (powerUpID);

    public void ActivatePowerUp()
    {
        OnSpeedAction();
    }
}
