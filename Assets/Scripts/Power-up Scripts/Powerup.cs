using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] float _speed = 3.0f;
    [SerializeField] int _powerupId;
    [SerializeField] AudioClip _powerupSfx;
    public float volume = 0.2f;
    public SpeedPowerUp _speedPu;
    public OptionsPowerUp _optionsPU;
    public ShieldPowerUp _shieldPU;



    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Player"))
            return;
        
        var player = other.transform.GetComponent<Player>();

        AudioSource.PlayClipAtPoint(_powerupSfx, Camera.main.transform.position, volume);

        if (player == null)
            return;
            
        switch (_powerupId)
        {
            case 0:
                player.TripleShotActive();
                break;
            case 1:
                _speedPu.SpeedBoost();

                break;
            case 2:
                _shieldPU.ShieldBoost();
                break;
            case 3:
                _optionsPU.ActivateOptions();
                break;
            default:
                Debug.Log("Default Value");
                break;
        }

        Destroy(this.gameObject); 

             


    }

}
