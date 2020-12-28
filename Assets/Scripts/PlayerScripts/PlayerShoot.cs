using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private Transform _firePoint;
    [SerializeField] GameObject singleShotPreFab;
    [SerializeField] GameObject doubleShotPreFab;
    [SerializeField] GameObject tripleShotPreFab;

    public  float _canFire = -1f;
    public float _fireRate = 0.12f;
    public int laserNumber;


    private void Start()
    {
        laserNumber = 0;
    }

    private void Update()
    {
        if (laserNumber < 0)
        {
            laserNumber = 0;
        }

        if (laserNumber > 2)
        {
            laserNumber = 2;
        }
        
    }

    public void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        ProjectileToFire();
    }

    private void ProjectileToFire()
    {
        var pos = _firePoint.position;
        var rot = Quaternion.identity;

        switch (laserNumber)
        {
            case 0:
                _fireRate = 0.12f;
                Instantiate(singleShotPreFab, pos, rot);
                break;

            case 1:
                _fireRate = 0.13f;
                Instantiate(doubleShotPreFab, pos, rot);
                break;

            case 2:
                _fireRate = 0.14f;
                Instantiate(tripleShotPreFab, pos, rot); 
                break;
        }
    }
}
