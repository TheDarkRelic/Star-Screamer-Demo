using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _explosionFx;
    private Player _player;
    [SerializeField] private GameObject _missilePreFab;
    public GameObject missileOptionsPreFab;
    [SerializeField] float _fireRate = 2.0f;
    [SerializeField] private float _delay = 1f;
    private GameObject _missile;
    private bool _isActive;
    private float _counter = 1;
    private HitDamage _hitDamage;


    void Start()
    {
        _hitDamage = FindObjectOfType<HitDamage>();
        Player.optionsActive = true;
        _isActive = true;
        _player = FindObjectOfType<Player>();
        StartCoroutine(SpawnMissilesDelay(_delay));
        StartCoroutine(IframeCounter(_counter));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.CompareTag("Powerup") && !other.CompareTag("Player") && !other.CompareTag("Missile") && !other.CompareTag("Laser"))
        {
            if (!_player)
            {
                _hitDamage.ProcessDamage(0);
            }

            if (!_isActive)
            {
                Instantiate(_explosionFx, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator SpawnMissiles(float fireRate)
    {
        while (true)
        {   
            _missile = Instantiate(_missilePreFab, transform.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator SpawnMissilesDelay(float delay)
    {
        yield return  new WaitForSeconds(delay);
        StartCoroutine(SpawnMissiles(_fireRate));
    }

    private IEnumerator IframeCounter(float counter)
    {
        yield return new WaitForSeconds(counter);
        _isActive = false;
    }

    public void ActivateOptions()
    {
        if (Player.optionsActive)
            return;

        Player.optionsActive = true;
        ActivateMissilePods();
    }

    private void ActivateMissilePods()
    {
        var options = (GameObject)Instantiate(missileOptionsPreFab, transform.position, Quaternion.identity);
        ParentToThisTransform(options);
    }

    private void ParentToThisTransform(GameObject objToParent)
    {
        objToParent.transform.parent = this.gameObject.transform;
    }

}
