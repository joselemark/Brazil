using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceController : MonoBehaviour
{
    public static SliceController _instance;

    public static SliceController Instance { get { return _instance; } }

    public SlicerPlane slicerPlane;    
    public Material crossSectionMaterial;

    private List<GameObject> objectsToSlice;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectsToSlice = new List<GameObject>(slicerPlane.objectsIntersecting);

            if (objectsToSlice.Count != 0)
            {
                SliceObjects(objectsToSlice, slicerPlane.plane, true);                     
            }
        }
    }

    public List<GameObject> SliceObject(GameObject objectToSlice, Plane slicerPlane, bool addPhysicalComponents)
    {
        GameObject[] objectsSliced = SliceUtils.SliceObjectWithCrossSection(objectToSlice, slicerPlane.ClosestPointOnPlane(Vector3.zero), slicerPlane.normal, SliceUtils.CalculateCustomRegion(crossSectionMaterial,0,0,512,512), crossSectionMaterial);

        if (objectsSliced == null)
        {
            Debug.LogWarning("SlicerPlane can't cut any object.");
            return null;
        }

        List<GameObject> slicedObjectsInstances = new List<GameObject>();

        foreach (GameObject objectSliced in objectsSliced)
        {
            GameObject objectInstance = Instantiate(objectSliced, objectToSlice.transform.position, objectToSlice.transform.rotation, 
                objectToSlice.transform.parent);
            objectInstance.tag = "Sliceable";
            slicedObjectsInstances.Add(objectInstance);
            Destroy(objectSliced);
        }

        Destroy(objectToSlice);

        if(addPhysicalComponents)
            AddPhysicalComponentsToObjects(slicedObjectsInstances);

        return slicedObjectsInstances;
    }  

    public List<GameObject> SliceObjects(List<GameObject> objectsToSlice, Plane slicerPlane, bool resultingObjectsPhysical)
    {
        List<GameObject> slicedObjects = new List<GameObject>();

        foreach(GameObject objectToSlice in objectsToSlice)
        {
            List<GameObject> slicedObjectnOneCut = SliceObject(objectToSlice, slicerPlane, resultingObjectsPhysical);

            if (slicedObjectnOneCut != null)
            {
                foreach (GameObject slicedObject in slicedObjectnOneCut)
                {
                    slicedObjects.Add(slicedObject);
                }
            }
        }

        return slicedObjects;
    }

    public void RemoveDestroyedObjectsFromList(List<GameObject> objectsList)
    {
        for (int i = objectsList.Count - 1; i > -1; i--)
        {
            if (objectsList[i] == null)
                objectsList.RemoveAt(i);
        }
    }

    private void AddPhysicalComponentsToObjects(List<GameObject> objects)
    {
        foreach(GameObject slicedObject in objects)
        {
            MeshCollider meshCollider;
            Rigidbody rigidbody;

            meshCollider = slicedObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            meshCollider.convex = true;

            rigidbody = slicedObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
    }
}
