using UnityEngine;

public class SpeedDown :  MonoBehaviour, IPowerup
{
    private PlayerMovement _playerMovement;
    [SerializeField] private int speedMinus = -1;

    void Awake()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public int AdjustAmount => speedMinus;

    public string PowerUpName => "Speed Down";

    public int PowerUpID => 2;


    public void ActivatePowerUp()
    {
        if (_playerMovement != null)
        {
            _playerMovement.AdjustSpeed(AdjustAmount);
        }
    }
}
