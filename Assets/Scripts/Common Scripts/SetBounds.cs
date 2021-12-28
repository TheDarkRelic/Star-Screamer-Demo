using UnityEngine;

public class SetBounds : MonoBehaviour
{
    public void ObjectBounds()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -4.15f, 5f));
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.1f, 5.1f), transform.position.y);
    }
}
