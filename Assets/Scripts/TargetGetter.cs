using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGetter : MonoBehaviour
{
    GameObject _player; // Must Set <T>
    public Transform target;
    public Enemy enemy;
    [SerializeField] private float _pingsPerSecond = 10;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            enemy.followsTarget = true;
            target = _player.transform;
        }
        StartCoroutine(TargetSetter(_player));
    }


    private IEnumerator TargetSetter(GameObject target)
    {
        if (this._player == null)
        {
            var followScripts = GetComponents<FollowTarget>();
            foreach (var script in followScripts)
            {
                Destroy(script);
            }
        }
        var ping = 0;
        var pingsPerSecond = _pingsPerSecond / 100;
        print("Ping" + ping);
        ping++;
        yield return new WaitForSeconds(pingsPerSecond);
    }
}


