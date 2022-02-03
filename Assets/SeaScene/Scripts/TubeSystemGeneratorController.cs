using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSystemGeneratorController : MonoBehaviour
{
    public TerrainGenerator terrainGenarator;


    public float eps = 0.2f;

    public GameObject TubePrefab;
    public List<GameObject> Tube_list;

    // Start is called before the first frame update
    void Start()
    {

        Tube_list = new List<GameObject>();
        if(Random.value < eps)
            AddTube(Tube_list);
    }

    void AddTube(List<GameObject> Tubes)
    {
        GameObject tube_inst = Instantiate(TubePrefab);
        float x = Random.Range(-terrainGenarator.xSegmentSize / 2.0f, terrainGenarator.xSegmentSize / 2.0f) + transform.position.x;
        float z = Random.Range(-terrainGenarator.zSegmentSize / 2.0f, terrainGenarator.zSegmentSize / 2.0f) + transform.position.z;
        float y = Height.GetHeightGlobal(x, z, terrainGenarator) + 5;
        tube_inst.transform.SetParent(transform);
        tube_inst.transform.position = new Vector3(x, y + transform.position.y, z);
        Tubes.Add(tube_inst);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
