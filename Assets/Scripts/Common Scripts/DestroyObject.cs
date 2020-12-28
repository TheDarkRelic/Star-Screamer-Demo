using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float _destroyTimer = .25f;
    void Start()
    {
        Destroy(this.gameObject, _destroyTimer);
    }
}
