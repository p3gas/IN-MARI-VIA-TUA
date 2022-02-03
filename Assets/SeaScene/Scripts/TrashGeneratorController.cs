using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGeneratorController : MonoBehaviour
{
    public TerrainGenerator terrainGenarator;


    public float nTrashes = 10;

    public GameObject[] TrashesPrefab;
    public List<GameObject> trashes_list;

    // Start is called before the first frame update
    void Start()
    {
        nTrashes = Random.Range(0, 10);
        trashes_list = new List<GameObject>();
        for (int i = 0; i < nTrashes; i++)
            AddTrash(trashes_list);
    }

    void AddTrash(List<GameObject> Trashes)
    {
        GameObject trash_inst = Instantiate(TrashesPrefab[Random.Range(0, TrashesPrefab.Length - 1)]);
        float x = Random.Range(-terrainGenarator.xSegmentSize / 2.0f, terrainGenarator.xSegmentSize / 2.0f) + transform.position.x;
        float z = Random.Range(-terrainGenarator.zSegmentSize / 2.0f, terrainGenarator.zSegmentSize / 2.0f) + transform.position.z;
        float y = Height.GetHeightGlobal(x, z, terrainGenarator);
        trash_inst.transform.SetParent(transform);
        trash_inst.transform.position = new Vector3(x, y + transform.position.y + 1.0f, z);
        Trashes.Add(trash_inst);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
