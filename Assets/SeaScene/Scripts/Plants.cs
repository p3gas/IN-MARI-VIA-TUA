using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public float xRange = 20;
    public float zRange = 20;
    public float nPlant1 = 10;
    public float nPlant2 = 10;
    public float nPlant3 = 10;

    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;

    public float Noise = 10.0f;
    public float xNoise = 0.05f;
    public float zNoise = 0.05f;
    public float xNoiseShift = 1000.0f;
    public float zNoiseShift = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nPlant1; i++)
        {
            GameObject plant = Instantiate(plant1);
            float x = Random.Range(-xRange, xRange);
            float z = Random.Range(-zRange, zRange);
            float y = Mathf.PerlinNoise(x * xNoise + xNoiseShift, z * zNoise + zNoiseShift) * Noise;
            y = y < 0 ? 0 : y;
            plant.transform.SetParent(transform);
            plant.transform.localPosition = new Vector3(x, y, z);
        }
        for (int i = 0; i < nPlant2; i++)
        {

        }
        for (int i = 0; i < nPlant3; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
