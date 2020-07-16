using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjectController : MonoBehaviour
{
    public Transform trackedObject;
    [Range(0f,1f)]
    public float smoothFactor = 0.5f;
    private Vector3 localPositionBeforeTracking;

    private void Start()
    {
        localPositionBeforeTracking = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (trackedObject != null)
        {
            //Smooth tracking
            transform.position = Vector3.Lerp(transform.position, trackedObject.position, smoothFactor * 0.1f);
        }        
        else
        {
            //Smooth return to the original position
            transform.localPosition = Vector3.Lerp(transform.localPosition, localPositionBeforeTracking, smoothFactor * 0.2f);
        }
            
    }

    public void SeTrackedObject(Transform objectToTrack)
    {        
        this.trackedObject = objectToTrack;
    }

    public void StopTracking()
    {
        this.trackedObject = null;
    }
}
