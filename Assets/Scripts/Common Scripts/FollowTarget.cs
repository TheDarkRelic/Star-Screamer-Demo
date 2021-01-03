using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Enemy enemy = null;
    [SerializeField] private TargetGetter _targetGetter = null;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _targetGetter._target.position, step);
    }
}
