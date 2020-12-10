using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public GameObject shieldVisuals;
    public bool isShieldActive = false;
    public void ShieldBoost()
    {
        isShieldActive = true;
        shieldVisuals.SetActive(true);
        print("shield is active");
    }
}
