using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private float _speed = 3f;

    private void Start()
    {
        SetState(MoveState.NotTracking);
        _preTarget = FindObjectOfType<Enemy>();
        Destroy(this.gameObject, 1.5f);
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
