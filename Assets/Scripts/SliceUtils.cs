using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public static class SliceUtils
{
    /**
     * Example on how to slice a GameObject in world coordinates.
     */
    public static SlicedHull SliceHull(GameObject objectToSlice, Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        return objectToSlice.Slice(planeWorldPosition, planeWorldDirection);
    }

    /**
     * Example on how to slice a GameObject in world coordinates.
     */
    public static GameObject[] SliceObject(GameObject objectToSlice, Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection);
    }

    /**
     * Example on how to slice a GameObject in world coordinates.
     * Uses a custom TextureRegion to offset the UV coordinates of the cross-section
     * Uses a custom Material
     */
    public static SlicedHull SliceHullWithCrossSection(GameObject objectToSlice, Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region, Material crossSectionMaterial)
    {
        return objectToSlice.Slice(planeWorldPosition, planeWorldDirection, region, crossSectionMaterial);
    }

    /**
     * Example on how to slice a GameObject in world coordinates.
     * Uses a custom TextureRegion to offset the UV coordinates of the cross-section
     * Uses a custom Material
     */
    public static GameObject[] SliceObjectWithCrossSection(GameObject objectToSlice, Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region, Material crossSectionMaterial)
    {
        return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, crossSectionMaterial);
    }

    /**
     * Example on how to calculate a custom TextureRegion to reference a different part of a texture
     * 
     * px -> The start X Position in Pixel Coordinates
     * py -> The start Y Position in Pixel Coordinates
     * width -> The width of the texture in Pixel Coordinates
     * height -> The height of the texture in Pixel Coordinates
     */
    public static TextureRegion CalculateCustomRegion(Texture myTexture, int px, int py, int width, int height)
    {
        return myTexture.GetTextureRegion(px, py, width, height);
    }

    /**
     * Example on how to calculate a custom TextureRegion to reference a different part of a texture
     * This example will use the mainTexture component of a Material
     * 
     * px -> The start X Position in Pixel Coordinates
     * py -> The start Y Position in Pixel Coordinates
     * width -> The width of the texture in Pixel Coordinates
     * height -> The height of the texture in Pixel Coordinates
     */
    public static TextureRegion CalculateCustomRegion(Material myMaterial, int px, int py, int width, int height)
    {
        return myMaterial.GetTextureRegion(px, py, width, height);
    }
}
