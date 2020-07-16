using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisionController : MonoBehaviour
{
    public GameObject targetVisionReference;

    private TrackObjectController trackObjectController;

    private void Start()
    {
        trackObjectController = targetVisionReference.GetComponent<TrackObjectController>();
        // We make sure there is always a TrackObjectController attached to the targetVisionReference
        if (trackObjectController == null)
        {
            Debug.LogWarning("VisionTargetReference doesn't have a TrackObjectController Component. One will be added");
            trackObjectController = targetVisionReference.AddComponent(typeof(TrackObjectController)) as TrackObjectController;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Look at object if it is a visible object and is closer than other visible object
        if (other.tag.Equals("VisiblyDetectableObject"))
        {
            if(trackObjectController.trackedObject == null)
                trackObjectController.SeTrackedObject(other.transform);
            else
            {
                float distanceToNewObject = (other.transform.position - transform.position).magnitude;
                float distanceToTrackedObject = (trackObjectController.trackedObject.transform.position - transform.position).magnitude;

                if(distanceToNewObject < distanceToTrackedObject)
                    trackObjectController.SeTrackedObject(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Stop looking when object tracked is not visible or there is an visible object closer
        if (other.tag.Equals("VisiblyDetectableObject") && trackObjectController.trackedObject != null)
        {
            if(trackObjectController.trackedObject.gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
                trackObjectController.StopTracking();
        }
    }
}
