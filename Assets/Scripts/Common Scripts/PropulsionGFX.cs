using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionGFX : MonoBehaviour
{

    [SerializeField] private Transform _propulsionSprite = null;
    private Vector3 normalScale;

    public PropulsionGFX(Transform propulsionSprite, Vector3 normalScale)
    {
        _propulsionSprite = propulsionSprite;
        this.normalScale = normalScale;
    }

    private void Awake()
    {
        normalScale = _propulsionSprite.localScale.normalized;
    }

    void Update()
    {
        ScaleThrusters();
    }

    private void ScaleThrusters()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            ScaleUpThrust();
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            ScaleDownThrust();
        }
        else
        {
            ThrustScaleDefault();
        }
    }

    private void ThrustScaleDefault()
    {
        normalScale = Vector3.one;
    }

    private void ScaleDownThrust()
    {
        normalScale = new Vector3(.5f, .5f, .5f);
    }

    private void ScaleUpThrust()
    {
        normalScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

}
