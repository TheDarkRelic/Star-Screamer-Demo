using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public AudioClip explosionSfx;
    [SerializeField] private float _explosionVolume = 0.3f;

    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSfx, Camera.main.transform.position, _explosionVolume);
        Destroy(this.gameObject, 2f);
    }


}
