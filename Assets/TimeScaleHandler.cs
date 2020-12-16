using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{
    [SerializeField] private float _timeScaleToAdjust = .1f;
    void Start()
    {
        SetTimeScale();
    }

    void SetTimeScale()
    {
        Time.timeScale = 1;
    }

    void AdjustTimeScale()
    {
        Time.timeScale = _timeScaleToAdjust;
    }

    private void OnEnable()
    {
        EventsList.OnPlayerDeath += AdjustTimeScale;
    }

    private void OnDisable()
    {
        EventsList.OnPlayerDeath -= AdjustTimeScale;
    }
}
