using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] AudioClip warningSfx = null;
    [SerializeField] float volume = .5f;

    private void PlayWarningSound()
    {
        AudioSource.PlayClipAtPoint(warningSfx, Camera.main.transform.position, volume);
    }
}
