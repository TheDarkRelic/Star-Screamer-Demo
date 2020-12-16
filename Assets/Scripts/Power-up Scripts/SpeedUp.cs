using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IPowerup
{
    private PlayerMovement _playerMovement;
    [SerializeField] private int speedPlus = 1;

    void Awake()
    {
        _playerMovement = GameObject.Find("Player_1").GetComponent<PlayerMovement>();
    }

    public int AdjustAmount => speedPlus;

    public string PowerUpName => "Speed Up";

    public int PowerUpID => 1;


    public void ActivatePowerUp()
    {
        if (_playerMovement != null)
        {
            _playerMovement.AdjustSpeed(AdjustAmount);
        }
    }
}
