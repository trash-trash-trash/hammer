using UnityEngine;

public class PerlinNoiseGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int width = 20;
    [SerializeField] private int depth = 20;
    [SerializeField] private float noiseScale = 0.1f;
    [SerializeField] private int heightMultiplier = 5;
    private float randomOffsetX;
    private float randomOffsetZ;

    public void GenerateTerrain()
    {
        // Remove previously generated terrain
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        randomOffsetX = Random.Range(0f, 10000f);
        randomOffsetZ = Random.Range(0f, 10000f);
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float noise = Mathf.PerlinNoise(
                    (x + randomOffsetX) * noiseScale,
                    (z + randomOffsetZ) * noiseScale
                );

                int height = Mathf.RoundToInt(noise * heightMultiplier);

                for (int y = 0; y <= height; y++)
                {
                    Vector3 position = transform.position + new Vector3(x, y, z);

                    GameObject cube;

                    if (cubePrefab != null)
                        cube = Instantiate(cubePrefab, position, Quaternion.identity, transform);
                    else
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    cube.transform.position = position;

                    if (cubePrefab == null)
                        cube.transform.SetParent(transform);
                }
            }
        }
    }
}