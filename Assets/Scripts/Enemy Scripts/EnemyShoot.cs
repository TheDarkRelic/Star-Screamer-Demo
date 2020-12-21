using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    float _canFire = -1.5f;
    [SerializeField] AudioClip _laserSfx;
    [SerializeField] GameObject _laserPreFab;
    [SerializeField] Transform _firePoint;


    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (Time.time > _canFire && GetComponent<BoxCollider2D>() != null)
        {
            var fireRate = Random.Range(0.5f, 2.0f);
            _canFire = Time.time + fireRate;
            GameObject enemyLaser = Instantiate(_laserPreFab, _firePoint.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    public void AnimShoot()
    {
        GameObject enemyLaser = Instantiate(_laserPreFab, _firePoint.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();
        }
    }
}
