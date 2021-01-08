using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosion : MonoBehaviour
{
    private AudioSource aSource;
    [SerializeField] AudioClip clip;

    private void Awake() => aSource = GetComponent<AudioSource>();

    void Start() => aSource.PlayOneShot(clip);
}
