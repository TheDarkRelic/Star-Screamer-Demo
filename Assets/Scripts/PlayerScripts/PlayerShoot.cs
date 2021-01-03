using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject singleShotPreFab = null;
    [SerializeField] private GameObject doubleShotPreFab = null;
    [SerializeField] private GameObject tripleShotPreFab = null;

    public  float _canFire = -1f;
    public float _fireRate = 0.12f;
    public int laserNumber;


    private void Start()
    {
        laserNumber = 0;
    }

    private void Update()
    {
        LaserLevelBounds();

    }

    private void LaserLevelBounds()
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
        var pos = firePoint.position;
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
