using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    void Start()
    {
        _speed = Random.Range(.25f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
          transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
          if (transform.position.y < -7f)
          {
              var x = Random.Range(-3.5f, 3.5f);
              transform.position = new Vector3(x, 7f, 0);
              _speed = Random.Range(.25f, 1f);
          }
    }

}
