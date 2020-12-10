using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissle : MonoBehaviour
{


    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private float _delay = 1.0f;
    List<GameObject> _enemiesToTarget = new List<GameObject>();
    private GameObject _enemyToTarget;
    private Vector3 _target;
    private float _step;

    //private float followX;
    //private float followY;
    private void Awake()
    {
        Destroy(this.gameObject, 1.5f);
        _step = _speed * Time.deltaTime;
    }

    void Update()
    {
        InvokeRepeating("CheckForEnemy", 0, 0.5f);
    }


    private void CheckForEnemy()
    {
        if (_enemyToTarget == null)
        {
            transform.Translate(Vector2.up * _step, Space.Self);
        }
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            _enemiesToTarget.Add(enemy.gameObject);
        }

        if (_enemiesToTarget.Count == 0)
            return;
        if (_enemyToTarget != null)
        {
            transform.Translate(Vector2.up * _step, Space.Self);
            _target = _enemyToTarget.transform.position;
        }
        _enemyToTarget = _enemiesToTarget[0];
        Vector2 objectPos = transform.position;
        _target.x = _target.x - objectPos.x;
        _target.y = _target.y - objectPos.y;
        float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        
    }
}
