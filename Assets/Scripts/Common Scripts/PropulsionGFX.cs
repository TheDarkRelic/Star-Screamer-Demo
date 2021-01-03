using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionGFX : MonoBehaviour
{

    [SerializeField] private Transform _propulsionSprite = null;

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
        _propulsionSprite.localScale = new Vector3(0.25f, 0.31f);
    }

    private void ScaleDownThrust()
    {
        _propulsionSprite.localScale = new Vector3(0.2f, 0.15f);
    }

    private void ScaleUpThrust()
    {
        _propulsionSprite.localScale = new Vector3(0.3f, 0.6f);
    }

}
