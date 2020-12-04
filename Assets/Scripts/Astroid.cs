using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    [SerializeField] float _rotSpeed = 3.0f;
    [SerializeField] GameObject _explosionPreFab;
    private Spawner _spawner;



    private void Start()
    {
        _spawner = GameObject.FindObjectOfType<Spawner>();

        if (_spawner == null)
        {
            Debug.LogError("Spawner is Null");
        }
        _spawner.StartSpawning();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            Instantiate(_explosionPreFab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawner.StartSpawning();
            Destroy(this.gameObject, 0.2f);
        }
    }
}
