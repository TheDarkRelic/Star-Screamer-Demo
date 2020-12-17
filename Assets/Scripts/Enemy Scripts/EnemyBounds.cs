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
    }
}
