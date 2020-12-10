using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaniateExplosion : MonoBehaviour
{

    public GameObject explosionFxPreFab;
    public void InitExplosion(GameObject obj)
    {
        Instantiate(explosionFxPreFab, obj.transform.position, Quaternion.identity);
    }

}
