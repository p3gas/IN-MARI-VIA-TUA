using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Transform observatorPosition;
    public Material terrainMaterial;
    public float viewDistance = 40.0f;

    public float Noise = 10.0f;
    public float xNoise = 0.1f;
    public float zNoise = 0.1f;
    public float xNoiseShift = 1000.0f;
    public float zNoiseShift = 1000.0f;
    public int xSegmentSize = 20;
    public int zSegmentSize = 20;
    public int xSegments = 3;
    public int zSegments = 3;

    private float NoiseOld;
    private float xNoiseOld;
    private float zNoiseOld;
    private float xNoiseShiftOld;
    private float zNoiseShiftOld;
    private int xSegmentSizeOld;
    private int zSegmentSizeOld;
    private int xSegmentsOld;
    private int zSegmentsOld;


    private GameObject[,] terrainTable;

    // Start is called before the first frame update
    void Start()
    {
        SaveOld();
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckOld())
        {
            SaveOld();
            ClearTerrain();
            GenerateTerrain();
        }
            

        for (int x = 0; x < xSegments; x++)
            for (int z = 0; z < zSegments; z++)
                if (terrainTable[x, z] != null)
                    terrainTable[x, z].SetActive(HorizontalDistance(observatorPosition, terrainTable[x, z].transform) > viewDistance ? false : true);
    }

    void ClearTerrain()
    {
        for (int x = 0; x < xSegments; x++)
            for (int z = 0; z < zSegments; z++)
                Destroy(terrainTable[x, z]);
    }

    void GenerateTerrain()
    {
        terrainTable = new GameObject[xSegments, zSegments];
        for (int x = 0; x < xSegments; x++)
        {
            for (int z = 0; z < zSegments; z++)
            {
                GameObject terrainFragment = new GameObject();
                terrainFragment.AddComponent<MeshFilter>();
                terrainFragment.AddComponent<MeshCollider>();
                terrainFragment.transform.localPosition = new Vector3((x - xSegments / 2) * xSegmentSize, transform.localPosition.y, (z - zSegments / 2) * zSegmentSize);
                terrainFragment.name = "Segment_" + (x - xSegments / 2) + "_" + (z - zSegments / 2);

                MeshRenderer meshRenderer = terrainFragment.AddComponent<MeshRenderer>();
                meshRenderer.material = terrainMaterial;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                MeshGenerator meshGenerator = terrainFragment.AddComponent<MeshGenerator>();
                meshGenerator.xSize = xSegmentSize;
                meshGenerator.zSize = zSegmentSize;
                meshGenerator.xNoise = xNoise;
                meshGenerator.zNoise = zNoise;
                meshGenerator.Noise = Noise;
                meshGenerator.xNoiseShift = (x - xSegments / 2) * xSegmentSize * xNoise + xNoiseShift;
                meshGenerator.zNoiseShift = (z - zSegments / 2) * zSegmentSize * zNoise + zNoiseShift;
                meshGenerator.hasCollider = true;

                terrainFragment.transform.SetParent(transform);
                terrainTable[x, z] = terrainFragment;
            }
        }
    }

    float HorizontalDistance(Transform objectA, Transform objectB)
    {
        return Mathf.Sqrt(Mathf.Pow(objectA.position.x - objectB.position.x, 2.0f) + Mathf.Pow(objectA.position.z - objectB.position.z, 2.0f));
    }

    void SaveOld()
    {
        NoiseOld = Noise;
        xNoiseOld = xNoise;
        zNoiseOld = zNoise;
        xNoiseShiftOld = xNoiseShift;
        zNoiseShiftOld = zNoiseShift;
        xSegmentSizeOld = xSegmentSize;
        zSegmentSizeOld = zSegmentSize;
        xSegmentsOld = xSegments;
        zSegmentsOld = zSegments;
    }

    bool CheckOld()
    {
        if (NoiseOld == Noise && xNoiseOld == xNoise && zNoiseOld == zNoise && xNoiseShiftOld == xNoiseShift && zNoiseShiftOld == zNoiseShift && xSegmentSizeOld == xSegmentSize && zSegmentSizeOld == zSegmentSize && xSegmentsOld == xSegments && zSegmentsOld == zSegments)
            return true;
        else
            return false;
    }
}
