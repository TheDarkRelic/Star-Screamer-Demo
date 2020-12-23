using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour, IPowerup
{
    private PlayerShoot _playerShoot;

    void Awake()
    {
        _playerShoot = GameObject.Find("Player_1").GetComponent<PlayerShoot>();
    }

    public void ActivatePowerUp()
    {
        _playerShoot.laserNumber++;
    }

}
