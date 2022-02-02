using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public TerrainGenerator TerrainGenerator;
    public MeshGeneratorBorder MeshGenerator;
    public Transform ObservatorPosition;

    long i = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSonar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSonar();
    }

    void UpdateSonar()
    {
        if (TerrainGenerator != null && MeshGenerator != null && ObservatorPosition != null)
        {
            MeshGenerator.xNoise = TerrainGenerator.xNoise;
            MeshGenerator.zNoise = TerrainGenerator.zNoise;
            MeshGenerator.xNoiseShift = TerrainGenerator.xNoiseShift + (ObservatorPosition.localPosition.x * TerrainGenerator.xNoise);
            MeshGenerator.zNoiseShift = TerrainGenerator.zNoiseShift + (ObservatorPosition.localPosition.z * TerrainGenerator.zNoise);
        }
    }
}
