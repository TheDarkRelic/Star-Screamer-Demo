using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] float _speed = 3.0f;
    [SerializeField] int powerupID;
    [SerializeField] AudioClip _powerupSFX;



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

        AudioSource.PlayClipAtPoint(_powerupSFX, Camera.main.transform.position);

        if (player == null)
            return;
            
        switch (powerupID)
        {
            case 0:
                player.TripleShotActive();
                break;
            case 1:
                player.SpeedBoost();
                break;
            case 2:
                player.ShieldBoost();
                break;
            default:
                Debug.Log("Default Value");
                break;
        }

        Destroy(this.gameObject); 

             


    }

}
