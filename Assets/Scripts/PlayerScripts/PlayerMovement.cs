using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public int _maxSpeed = 10;
    public int _minSpeed = 4;
    public bool isActive = false; 
    void OnAwake() => speed = 6;

    public void CalculateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, y, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void AdjustSpeed(int adjustAmount)
    {
        if (adjustAmount < 0 && speed > _minSpeed)
        {
            speed += adjustAmount;
        }
        if (adjustAmount > 0 && speed < _maxSpeed)
        {
            speed += adjustAmount;
        }
        
    }

}
