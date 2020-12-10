using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHolder : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
