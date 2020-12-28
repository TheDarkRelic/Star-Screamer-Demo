using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] private float _rotSpeed = 50;

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed);
    }
}
