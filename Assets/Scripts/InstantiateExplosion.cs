using UnityEngine;

public class InstantiateExplosion : MonoBehaviour
{
    public  GameObject explosionFxPreFab;
    public  void InitExplosion(GameObject obj)
    {
        Instantiate(explosionFxPreFab, obj.transform.position, Quaternion.identity);
    }

}
