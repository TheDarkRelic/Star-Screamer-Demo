using UnityEngine;
using System.Collections;

public class InstantiateExplosion : MonoBehaviour
{
    private bool canSpawn = true;
    public  GameObject explosionFxPreFab;
    public  void InitExplosion(GameObject obj)
    {
        if (canSpawn)
        {
            CanSpawn();
            Instantiate(explosionFxPreFab, obj.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator CanSpawn()
    {
        canSpawn = false;
        yield return new WaitForEndOfFrame();
        canSpawn = true;
    }

}
