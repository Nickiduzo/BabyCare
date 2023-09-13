using System;
using Unity.Mathematics;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private CloudPool pool;

    private Vector3 priviousPosition;
    // Start work before starts play and set position for cloud
    private void Awake()
    {
        priviousPosition = new Vector3(UnityEngine.Random.Range(-25f, -10f), UnityEngine.Random.Range(5f, -5f));
    }
    //Spawn Clouds by random position and use pool
    public Cloud[] SpawnCloud(Cloud[] clouds)
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i] = pool.Pool[i].GetFreeElement();
            clouds[i].transform.position = GetRandomPosition();
        }
        return clouds;
    }
    //Respawn Clouds by random position and use pool
    public Cloud[] RespawnClouds(Cloud[] clouds)
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            if (IsNotActive(clouds[i]))
            {
                clouds[i] = pool.Pool[i].GetFreeElement();
                clouds[i].transform.position = GetRandomPosition();
            }
        }
        return clouds;
    }
    // Check that cloud is not active in hierarchy
    private bool IsNotActive(Cloud cloud)
    {
        if (!cloud.gameObject.activeSelf) return true;
        return false;
    }
    // Set random position by -25f to -10f by x position and 5 to -5 by y position
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            randomPosition = new Vector3(UnityEngine.Random.Range(-25f, -10f), UnityEngine.Random.Range(5f, -5f));
            validPosition = true;
            
            //check that position of previous and next cloud are different
            if (Vector3.Distance(randomPosition, priviousPosition) < 5f)
            {
                validPosition = false;
            }
        }
        priviousPosition = randomPosition;
        return randomPosition;    
    }
}
