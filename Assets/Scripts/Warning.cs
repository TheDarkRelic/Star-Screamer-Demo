using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] AudioSource aSource;


    private void PlayWarningSound()
    {
        aSource.Play();
    }
}
