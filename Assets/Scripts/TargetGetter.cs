using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGetter : MonoBehaviour
{
    
    [HideInInspector] public Transform _target;
    
    [HideInInspector] public Enemy enemy;
    [SerializeField] private float _pingsPerSecond = 10;


    void Start()
    {
        _target = FindObjectOfType<Player>().gameObject.transform;
        if (_target != null)
        {
            enemy.followsTarget = true;
        }

    }

}


