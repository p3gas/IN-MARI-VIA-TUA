using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public Mesh mesh;
    public bool hasCollider;
    private MeshCollider meshCollider;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    public float xNoise = 0.3f;
    public float zNoise = 0.3f;
    public float xNoiseShift = 0f;
    public float zNoiseShift = 0f;
    public float Noise = 10.0f;

    private int xSizeOld;
    private int zSizeOld;
    private float xNoiseOld;
    private float zNoiseOld;
    private float xNoiseShiftOld;
    private float zNoiseShiftOld;
    private float NoiseOld;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        if (hasCollider)
        {
            meshCollider = GetComponent<MeshCollider>();
            if (meshCollider)
                meshCollider.sharedMesh = mesh;
        }

        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateShape())
            UpdateMesh();
    }

    void CreateShape()
    {
        xSize = xSize <= 0 ? 1 : xSize;
        zSize = zSize <= 0 ? 1 : zSize;
        SaveOld();

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uvs = new Vector2[(xSize + 1) * (zSize + 1)];

        GenerateVetrices();

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    bool UpdateShape()
    {
        if (!CheckOld())
        {
            if (xSize == xSizeOld && zSize == zSizeOld)
                GenerateVetrices();
            else
                CreateShape();
            SaveOld();
            return true;
        }
        else
            return false;
    }

    void GenerateVetrices()
    {
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * xNoise + xNoiseShift, z * zNoise + zNoiseShift) * Noise;
                y = y < 0 ? 0 : y;
                vertices[i] = new Vector3(x - xSize / 2, y, z - zSize / 2);
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        if (meshCollider)
        {
            meshCollider.enabled = false;
            meshCollider.enabled = true;
        }
    }

    void SaveOld()
    {
        xSizeOld = xSize;
        zSizeOld = zSize;
        xNoiseOld = xNoise;
        zNoiseOld = zNoise;
        xNoiseShiftOld = xNoiseShift;
        zNoiseShiftOld = zNoiseShift;
        NoiseOld = Noise;
    }

    bool CheckOld()
    {
        if (xSizeOld == xSize && zSizeOld == zSize && xNoiseOld == xNoise && zNoiseOld == zNoise && xNoiseShiftOld == xNoiseShift && zNoiseShiftOld == zNoiseShift && NoiseOld == Noise)
            return true;
        else
            return false;
    }

}
