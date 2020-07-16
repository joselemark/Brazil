using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerPlane : MonoBehaviour
{
    public Plane plane;
    public List<GameObject> objectsIntersecting;

    private void Awake()
    {
        plane.SetNormalAndPosition(transform.up, transform.position);
    }

    private void Update()
    {
        //When the object is sliced, the original object will be destroy, so we have to remove it from the list
        SliceController._instance.RemoveDestroyedObjectsFromList(objectsIntersecting);
    }

    private void FixedUpdate()
    {
        plane.SetNormalAndPosition(transform.up, transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!objectsIntersecting.Contains(other.gameObject) && other.tag.Equals("Sliceable"))
            objectsIntersecting.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsIntersecting.Remove(other.gameObject);
    }
}
