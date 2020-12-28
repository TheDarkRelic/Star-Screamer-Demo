using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour, IPowerup
{
    private Player.Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player.Player>();
    }

    public void ActivatePowerUp()
    {
        _player.playerShoot.laserNumber++;
    }

}
