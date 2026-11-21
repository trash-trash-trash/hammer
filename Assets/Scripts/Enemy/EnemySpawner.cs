using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public float spawnInterval = 5f;

    private NavMeshTriangulation navMeshData;
    private float[] triangleAreas;
    private float totalArea;

    public GameObject hammerObjPrefab;

    private void Start()
    {
        CalculateNavMeshAreas();

        StartCoroutine(SpawnEnemies());
    }

    private void CalculateNavMeshAreas()
    {
        navMeshData = NavMesh.CalculateTriangulation();

        int triangleCount = navMeshData.indices.Length / 3;

        triangleAreas = new float[triangleCount];
        totalArea = 0f;

        for (int i = 0; i < triangleCount; i++)
        {
            int index = i * 3;

            Vector3 vertex1 =
                navMeshData.vertices[navMeshData.indices[index]];

            Vector3 vertex2 =
                navMeshData.vertices[navMeshData.indices[index + 1]];

            Vector3 vertex3 =
                navMeshData.vertices[navMeshData.indices[index + 2]];

            float area = Vector3.Cross(
                vertex2 - vertex1,
                vertex3 - vertex1
            ).magnitude * 0.5f;

            triangleAreas[i] = area;
            totalArea += area;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        int triangleIndex = GetRandomTriangleByArea();

        int index = triangleIndex * 3;

        Vector3 vertex1 =
            navMeshData.vertices[navMeshData.indices[index]];

        Vector3 vertex2 =
            navMeshData.vertices[navMeshData.indices[index + 1]];

        Vector3 vertex3 =
            navMeshData.vertices[navMeshData.indices[index + 2]];

        Vector3 randomPosition =
            GetRandomPointInTriangle(vertex1, vertex2, vertex3);

        GameObject enemy = Instantiate(
            enemyPrefab,
            randomPosition,
            Quaternion.identity
        );

        enemy.transform.parent = transform;
        
        GameObject hammer = Instantiate(hammerObjPrefab);
        HammerBrain hammerBrain = hammer.GetComponent<HammerBrain>();
        hammerBrain.playerTransform = enemy.transform;
        
        //equip hammer to enemy
        EnemyBrain enemyBrain = enemy.GetComponent<EnemyBrain>();
        enemyBrain.hammerBrain = hammerBrain;
        
        spawnedEnemies.Add(enemy);
    }

    private int GetRandomTriangleByArea()
    {
        float randomValue = Random.Range(0f, totalArea);
        float currentArea = 0f;

        for (int i = 0; i < triangleAreas.Length; i++)
        {
            currentArea += triangleAreas[i];

            if (randomValue <= currentArea)
                return i;
        }

        return triangleAreas.Length - 1;
    }

    private Vector3 GetRandomPointInTriangle(
        Vector3 vertex1,
        Vector3 vertex2,
        Vector3 vertex3)
    {
        float random1 = Random.value;
        float random2 = Random.value;

        if (random1 + random2 > 1f)
        {
            random1 = 1f - random1;
            random2 = 1f - random2;
        }

        return vertex1 +
               random1 * (vertex2 - vertex1) +
               random2 * (vertex3 - vertex1);
    }
}