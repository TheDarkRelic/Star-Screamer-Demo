using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public float speed;
    public float x, y, z;


    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(x, y,z) * speed * Time.deltaTime);
    }
}
