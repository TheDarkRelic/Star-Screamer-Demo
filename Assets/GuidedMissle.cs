using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissle : MonoBehaviour
{


    [SerializeField] private float _speed = 8.0f;
    private float _step;
    private bool isTracking = false;
    [SerializeField] private float _delay = 1.0f;
    List<GameObject> enemiesToTarget = new List<GameObject>();
    private GameObject enemyToTarget;
    private Vector3 _target;

    //private float followX;
    //private float followY;
    private void Awake()
    {
        Destroy(this.gameObject, 1f);
        _step = _speed * Time.deltaTime;
    }
    void Start()
    {
        StartCoroutine(MissleDelay());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _step, Space.Self);
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemiesToTarget.Add(enemy.gameObject);
        }

        if (enemiesToTarget.Count != 0)
        {
            if (enemyToTarget != null)
            {
               _target = enemyToTarget.transform.position;
            }
            enemyToTarget = enemiesToTarget[0];
            Vector2 objectPos = transform.position; 
            _target.x = _target.x - objectPos.x;
            _target.y = _target.y - objectPos.y;
            float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            //followX = _target.x;
            //followY = _target.y;
            //var followTarget = new Vector2(followX, followY);
            //transform.position = Vector2.MoveTowards(transform.position, followTarget, _step);
        }

    }

    private IEnumerator MissleDelay()
    {
        isTracking = false;
        yield return new WaitForSeconds(.2f);
        isTracking = true;
    }
}
