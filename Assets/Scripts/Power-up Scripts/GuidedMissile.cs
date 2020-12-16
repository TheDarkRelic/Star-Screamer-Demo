using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }

    private Transform _target;
    public float offset = -90;

    private void Update()
    {
        transform.Translate(Vector2.up * (5 * Time.deltaTime), Space.Self);
        var enemyTargets = FindObjectsOfType<Enemy>();
        _target = enemyTargets[0].gameObject.transform;

        var guidedMissile = GetComponent<GuidedMissile>();

        var step = 5 * Time.deltaTime;
        var targetPos = _target.position;
        var thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        var angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.position = Vector2.MoveTowards(transform.position, _target.position, step);

    }
   

}
