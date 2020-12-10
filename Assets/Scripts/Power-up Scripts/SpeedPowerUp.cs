using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] float _speedBoost = 5f;
    [SerializeField] float _speedCap = 15f;
    PlayerMovement _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        if (_player == null)
        {
           Debug.Log("Not Found Player");

        }
    }

    public void SpeedBoost()
    {
        if (_player.speed >= _speedCap)
        {
            _player.speed = _speedCap;
        }
        else
        { 
            _player.speed += _speedBoost;
        }
    }
}
