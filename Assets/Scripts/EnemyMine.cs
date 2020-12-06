
using UnityEngine;

public class EnemyMine : MonoBehaviour
{
    private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSeed;
    private Transform _target;

    void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSeed);
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        var step = _speed * Time.deltaTime;
        if (_target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, step);
        }
             
    }
}
