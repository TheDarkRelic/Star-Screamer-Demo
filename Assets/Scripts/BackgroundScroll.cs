using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _speed = .001f;

    void Update()
    {
        if (transform.position.y < -67)
        {
            transform.position = new Vector3(0, 67f, 0);
        }
        transform.Translate(Vector3.down * _speed * Time.deltaTime,Space.World);
    }
}
