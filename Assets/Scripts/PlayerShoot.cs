using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] GameObject laserPreFab;
    [SerializeField] GameObject tripleShotPreFab;

    public  float _canFire = -1f;
    [SerializeField] float _fireRate = .05f;

    public bool isTripleShotActive = false;

    public void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        ProjectileToFire();
    }

    private void ProjectileToFire()
    {
        if (isTripleShotActive == true)
        {
            Instantiate(tripleShotPreFab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPreFab, _firePoint.position, Quaternion.identity);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
    }

    public void TripleShotInActive()
    {
        isTripleShotActive = false;
    }
}
