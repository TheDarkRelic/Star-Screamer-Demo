using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IPowerup
{
    private PlayerMovement _playerMovement;
    [SerializeField] private int speedPlus = 1;

    void Awake()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public int AdjustAmount => speedPlus;

    public void ActivatePowerUp()
    {
        if (_playerMovement == null)
            return;
        _playerMovement.AdjustSpeed(AdjustAmount);
    }
}
