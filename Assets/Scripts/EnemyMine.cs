
using UnityEngine;

public class EnemyMine : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _target;

    void Start()
    {
        _speed = Random.Range(5, 8);
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
