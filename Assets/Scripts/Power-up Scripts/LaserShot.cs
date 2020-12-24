using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour, IPowerup
{
    private Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void ActivatePowerUp()
    {
        _player.playerShoot.laserNumber++;
    }

}
