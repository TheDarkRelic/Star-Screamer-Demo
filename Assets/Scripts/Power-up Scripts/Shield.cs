using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public static bool isShieldActive = false;

    void ActivateShield()
    {
        var player = FindObjectOfType<Player.Player>();
        player.OnShieldActivate.Invoke();
        var hitDamage = GetComponent<HitDamage>();
        hitDamage.isDamageable = false;
    }
}
