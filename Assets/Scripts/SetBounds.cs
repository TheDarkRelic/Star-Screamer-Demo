using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBounds : MonoBehaviour
{
    public void ObjectBounds()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -4.15f, 5f));
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.7f, 2.7f), transform.position.y);
;
       if (transform.position.x > 2.7f)
        {
            transform.position = new Vector3(2.7f, transform.position.y, 0);
        }
        else if (transform.position.x < -2.7f)
        {
            transform.position = new Vector3(-2.7f, transform.position.y, 0);
        }
    }
}
