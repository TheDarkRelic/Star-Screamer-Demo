using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionSfx;
    [SerializeField] private float _ExplosionVolume;

    void Start()
    {

        AudioSource.PlayClipAtPoint(explosionSfx, Camera.main.transform.position, _ExplosionVolume);
        Destroy(this.gameObject, 2f);
    }


}
