using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 0.1f;

    private void LateUpdate()
    {
        FollowTarget();
    }
    
    public void SelectTarget(Transform target)
    {
        this.target = target;
    }

    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 desiredPostion = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPostion, smoothSpeed);
            transform.position = smoothedPos;
        }
    }

}
