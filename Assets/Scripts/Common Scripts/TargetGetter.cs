using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGetter : MonoBehaviour
{
    [HideInInspector] public Transform _target;
    
    void Start()
    {
        var target = FindObjectOfType<Player>().gameObject.transform;
        if (target != null)
        {
            _target = target;
        }

    }

}


