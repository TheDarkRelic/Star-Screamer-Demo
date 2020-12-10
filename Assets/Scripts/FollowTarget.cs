using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TargetGetter _targetGetter;
    void Update()
    {
        if (_enemy.followsTarget)
        {
            var step = _enemy.speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _targetGetter.target.position, step);
        }
    }
}
