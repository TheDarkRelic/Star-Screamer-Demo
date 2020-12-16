using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown2D : MonoBehaviour
{
    public float metersPerSecond = 5f;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        var speed = metersPerSecond * Time.deltaTime;
        transform.Translate(Vector2.down * speed);
    }
}
