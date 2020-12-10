using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public void CalculateMovement()
    {

        float horizontalInput = Input.GetAxis("P1Horizontal");
        float verticalInput = Input.GetAxis("P1Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        
    }
}
