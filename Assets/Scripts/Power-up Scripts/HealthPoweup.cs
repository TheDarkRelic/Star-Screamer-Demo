using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoweup : MonoBehaviour, IPowerup
{
    [SerializeField] int _maxHealth;
    public void ActivatePowerUp()
    {
        var player = FindObjectOfType<Player.Player>();

        if (player == null)
            return;

        if (player.hitDamage.health < _maxHealth) player.hitDamage.health++;
        EventsList.OnHealthPickup?.Invoke(player.hitDamage.health);
    }
}
