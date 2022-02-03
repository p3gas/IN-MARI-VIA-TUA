using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public TerrainGenerator terrainGenarator;

    public float nPlant1 = 10;
    public float nPlant2 = 10;
    public float nPlant3 = 10;

    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;

    public List<GameObject> plants1;
    public List<GameObject> plants2;
    public List<GameObject> plants3;

    // Start is called before the first frame update
    void Start()
    {
        plants1 = new List<GameObject>();
        plants2 = new List<GameObject>();
        plants3 = new List<GameObject>();
        for (int i = 0; i < nPlant1; i++)
            AddPlant(plants1, plant1);
        for (int i = 0; i < nPlant2; i++)
            AddPlant(plants2, plant2);
        for (int i = 0; i < nPlant3; i++)
            AddPlant(plants3, plant3);
    }

    void AddPlant(List<GameObject> plants, GameObject plantPrefab)
    {
        GameObject plant = Instantiate(plantPrefab);
        float x = Random.Range(-terrainGenarator.xSegmentSize / 2.0f, terrainGenarator.xSegmentSize / 2.0f) + transform.position.x;
        float z = Random.Range(-terrainGenarator.zSegmentSize / 2.0f, terrainGenarator.zSegmentSize / 2.0f) + transform.position.z;
        float y = Height.GetHeightGlobal(x, z, terrainGenarator);
        plant.transform.SetParent(transform);
        plant.transform.position = new Vector3(x, y + transform.position.y, z);
        plants.Add(plant);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
