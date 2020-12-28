using System.Collections;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
[System.Serializable]
public class GuidedMissile : MonoBehaviour
{
    public enum MoveState
    {
        Tracking,
        NotTracking
    };
    
    public MoveState moveState;
    private Transform _target = null;
    private Enemy _preTarget = null;
    public float offset = -90;

    [SerializeField] private float _lifeTime = 2f;

    private void Start()
    {
        SetState(MoveState.NotTracking); 
        InvokeRepeating("GetTarget", 0, 0.2f);
        Destroy(this.gameObject, _lifeTime);
    }

    private void GetTarget()
    {
        _preTarget = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        
        if (_preTarget != null)
        {
            
            SetState(MoveState.Tracking);
        }
        else
        {
            SetState(MoveState.NotTracking);
        }

        switch (moveState)
        {
            case MoveState.Tracking:
                FaceAndFollowTarget();
                break;
            case MoveState.NotTracking:
                break;
        }
        
      
    }

    private void FaceAndFollowTarget()
    {
        _target = _preTarget.transform;
        var targetPos = _target.position;
        var thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        var angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }

    void SetState(MoveState state)
    {
        moveState = state;
    }
}
