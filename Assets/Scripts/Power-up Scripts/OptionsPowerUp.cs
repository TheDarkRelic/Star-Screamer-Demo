using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsPowerUp : MonoBehaviour, IPowerup
{
    
    public string PowerUpName { get; set; }

    public int PowerUpID { get; set; }

    public void ActivatePowerUp()
    {
        var events = FindObjectOfType<EventsList>();
        events.OnOptionsPickUp.Invoke();
        Destroy(this.gameObject);
    }

}

