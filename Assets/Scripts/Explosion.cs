using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip _explosionSfx;
    [SerializeField] private float _ExplosionVolume;

    void Start()
    {
        PlayExplosionAudio(_explosionSfx);
    }

    private void PlayExplosionAudio(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, _ExplosionVolume);
    }
}
