using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IPowerup
{
    public static Action OnSpeedAction;

    public void ActivatePowerUp()
    {
        OnSpeedAction();
    }
}
