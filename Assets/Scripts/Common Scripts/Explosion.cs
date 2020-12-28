using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip _explosionSfx;
    [SerializeField] private float _ExplosionVolume;

    void Start() => PlayExplosionAudio(_explosionSfx);

    private void PlayExplosionAudio(AudioClip clip)
    {
        var cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, _ExplosionVolume);
    }
}
