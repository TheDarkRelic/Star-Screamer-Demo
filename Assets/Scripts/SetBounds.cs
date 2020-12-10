using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBounds : MonoBehaviour
{
    public void ObjectBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.3f, 5f), 0);

        if (transform.position.x > 2.8f)
        {
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }
        else if (transform.position.x < -2.8f)
        {
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }
    }
}
