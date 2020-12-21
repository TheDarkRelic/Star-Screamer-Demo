using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounds : MonoBehaviour
{

    private void Update()
    {
        BoundsActive();
    }
    public void BoundsActive()
    {
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x < -4 || transform.position.x > 4)
        {
            Destroy(this.gameObject);
        }
    }
}
