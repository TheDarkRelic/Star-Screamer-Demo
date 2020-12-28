using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TargetGetter _targetGetter;
    [SerializeField] private float _speed;
    void Update()
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _targetGetter._target.position, step);
    }
}
