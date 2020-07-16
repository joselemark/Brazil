using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestroyer : MonoBehaviour
{
    public bool destroyObject = false;
    [Range(1,4)]
    public int numberOfCuts = 1;
    public bool addPhysicalComponents = true;

    public enum DestructionCenterModes { MeshCenter, RandomCenter, MultipleCenters}
    public DestructionCenterModes destructionCenterMode;

    private void FixedUpdate()
    {
        if (destroyObject)
        {
            if(SliceController._instance != null)
            {
                destroyObject = false;
                DestroyMesh(GetDestructionCenters(destructionCenterMode), addPhysicalComponents);
            }
            else
                Debug.LogError("A SliceController doesn't exists in the scene. Add a SliceController to make MeshDestroyer works.");
        }
    }

    private void DestroyMesh(List<Vector3> destructionCenters, bool addPhysicalComponents)
    {        
        List<GameObject> objectsToSlice = new List<GameObject>();

        objectsToSlice.Add(this.gameObject);

        for (int i=0; i < numberOfCuts; i++)
        {
            List<GameObject> slicedObjects = new List<GameObject>();
            
            Plane slicerPlane = new Plane(new Vector3(Random.Range(1,10) * 0.1f, Random.Range(1, 10) * 0.1f, Random.Range(1, 10) * 0.1f), destructionCenters[i]);
            slicedObjects = SliceController._instance.SliceObjects(objectsToSlice, slicerPlane, addPhysicalComponents);

            objectsToSlice = slicedObjects;
        }        
    }

    public List<Vector3> GetDestructionCenters(DestructionCenterModes destructionCenterMode)
    {
        List<Vector3> destructionCenters = new List<Vector3>();
        Renderer renderer = GetComponent<Renderer>();
        Bounds bounds;

        if (GetComponent<Collider>() == null)
        {
            MeshCollider meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            meshCollider.convex = true;
        }

        bounds = GetComponent<Collider>().bounds;

        if (destructionCenterMode == DestructionCenterModes.MeshCenter)
        {
            for(int i=0; i < numberOfCuts; i++)
            {
                destructionCenters.Add(renderer.bounds.center);
            }
            
            return destructionCenters;
        }
        else if(destructionCenterMode == DestructionCenterModes.RandomCenter)
        {
            Vector3 center = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                               Random.Range(bounds.min.y, bounds.max.y),
                               Random.Range(bounds.min.z, bounds.max.z));

            for (int i = 0; i < numberOfCuts; i++)
            {
                destructionCenters.Add(center);
            }

            return destructionCenters;
        }
        else
        {
            for (int i = 0; i < numberOfCuts; i++)
            {
                destructionCenters.Add(new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                               Random.Range(bounds.min.y, bounds.max.y),
                               Random.Range(bounds.min.z, bounds.max.z)));
            }

            return destructionCenters;
        }
    }
}
