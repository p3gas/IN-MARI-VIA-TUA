using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Height
{
    public static float GetHeight(float x, float z, float Noise = 10.0f)
    {
        float y = Mathf.PerlinNoise(x, z) * Noise;
        y = y < 0 ? 0 : y;
        return y;
    }

    public static float GetHeightGlobal(float xGlobal, float zGlobal, TerrainGenerator terrainGenerator)
    {
        return GetHeight((xGlobal * terrainGenerator.xNoise + terrainGenerator.xNoiseShift), (zGlobal * terrainGenerator.zNoise + terrainGenerator.zNoiseShift), terrainGenerator.Noise);
    }
}
