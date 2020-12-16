using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsObject : MonoBehaviour
{
    [SerializeField] private GameObject _missilePreFab;
    [SerializeField] float _fireRate = 2.0f;
    [SerializeField] private float _delay = 1f;
    private GameObject _missile;
    private float _counter = 1;
    [SerializeField] Transform _firePointLeft;
    [SerializeField] Transform _firePointRight;
    public bool isRight;
    public bool isLeft;



    void Start()
    {
        StartCoroutine(IframeDelay(_counter));
    }

    private void Update()
    {
        FireMissile(_missilePreFab);
    }

    public void FireMissile(GameObject missile)
    {
        SpawnMissiles(missile);
    }

    void SpawnMissiles(GameObject missile)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isLeft)
            {
                _missile = Instantiate(missile, _firePointLeft.position, Quaternion.identity) as GameObject;
            }

            if (isRight)
            {
                _missile = Instantiate(missile, _firePointRight.position, Quaternion.identity) as GameObject;
            }
        }
    }

    private IEnumerator IframeDelay(float counter)
    {
        yield return new WaitForSeconds(counter);
    }


}
