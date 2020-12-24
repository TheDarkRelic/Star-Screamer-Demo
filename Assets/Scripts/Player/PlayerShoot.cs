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
    [SerializeField] float _fireRate = .05f;
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
        switch (laserNumber)
        {
            case 0:
                Instantiate(singleShotPreFab, _firePoint.position, Quaternion.identity);
                break;

            case 1:
                Instantiate(doubleShotPreFab, transform.position, Quaternion.identity);
                break;

            case 2: 
                Instantiate(tripleShotPreFab, transform.position, Quaternion.identity); 
                break;
        }
    }
}
