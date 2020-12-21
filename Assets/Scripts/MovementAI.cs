using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public enum SpaceSelection
    {
        Self,
        World
    };

    public SpaceSelection spaceSelection;
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
        switch (spaceSelection)
        {
            case SpaceSelection.Self:
                transform.Translate(new Vector3(x, y, z) * speed * Time.deltaTime, Space.Self);
                break;
            case SpaceSelection.World:
                transform.Translate(new Vector3(x, y, z) * speed * Time.deltaTime, Space.World);
                break;
        }
        
    }
}
