using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] AudioClip _explosionSFX;
    [SerializeField] private float _explosionVolume = 0.5f;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (_explosionSFX == null)
            return;

        _audioSource.PlayOneShot(_explosionSFX, _explosionVolume);
        Destroy(this.gameObject, 2f);
    }


}
