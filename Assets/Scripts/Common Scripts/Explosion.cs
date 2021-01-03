using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosion : MonoBehaviour
{
    private List<int> Samples = new List<int>();
    public AudioClip explosionSfx = null;
    [SerializeField] private float explosionVolume = 0.3f;

    void Start() => PlayExplosionAudio(explosionSfx);

    private void PlayExplosionAudio(AudioClip clip)
    {
        var cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, explosionVolume);
    }
}
