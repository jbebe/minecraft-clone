using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int Size;
    public int Height;
    public float Roughness;
    public GameObject TopBlock;
    public GameObject MiddleBlock;

    // Start is called before the first frame update
    void Start()
    {
        var seed = Random.Range(0f, 1f);
        GenerateTerrain(seed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTerrain(float seed)
    {
        if (TopBlock == null)
            throw new UnityException("Blocks missing");

        var env = GameObject.FindGameObjectWithTag("Environment");
        var normalizer = (seed + Size);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                var k = Roughness * Mathf.PerlinNoise((i + seed) / normalizer, (j + seed) / normalizer) + Height;
                k = (int)k; // Truncate to integers
                var defaultBlock = Instantiate(TopBlock, new Vector3(i, k, j), Quaternion.identity);
                defaultBlock.transform.SetParent(env.transform);

                for (int h = 0; h < k; ++h)
                {
                    var dirtBlock = Instantiate(MiddleBlock, new Vector3(i, h, j), Quaternion.identity);
                }
            }
        }
    }
}
