using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject randomObstacleGO;
    [SerializeField] Transform lowerBoundary;
    [SerializeField] Transform upperBoundary;
    [SerializeField] int amountOfObstaclesToSpawn = 100;

    void Start()
    {
        if (randomObstacleGO == null)
        {
            Debug.LogError("No obstacle gameobject prefab has been given");
            return;
        }

        if (lowerBoundary == null || upperBoundary == null)
        {
            CreateBoundaryObjects();
        }

        for (int i = 0; i < amountOfObstaclesToSpawn; i++)
        {
            Vector3 randomPos;
            randomPos.x = Random.Range(lowerBoundary.position.x, upperBoundary.position.x);
            randomPos.y = Random.Range(lowerBoundary.position.y, upperBoundary.position.y);
            randomPos.z = Random.Range(lowerBoundary.position.z, upperBoundary.position.z);
            Instantiate(randomObstacleGO, randomPos, Quaternion.identity);
        }
    }

    [ContextMenu("Create Boundary Transforms")]
    void CreateBoundaryObjects()
    {
        Vector3 offset = new Vector3(20, 20, 20);

        GameObject lowerGO = new GameObject("LowerBoundary");
        lowerGO.transform.parent = this.transform;
        lowerGO.transform.position -= offset;
        lowerBoundary = lowerGO.transform;

        GameObject upperGO = new GameObject("UpperBoundary");
        upperGO.transform.parent = this.transform;
        upperGO.transform.position += offset;
        upperBoundary = upperGO.transform;
    }

}
