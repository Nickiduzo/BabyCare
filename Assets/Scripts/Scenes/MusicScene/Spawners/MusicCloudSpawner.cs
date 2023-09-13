using System;
using UnityEngine;

public class MusicCloudSpawner : MonoBehaviour
{
    [SerializeField] private MusicCloudPool pool;

    public Cloud[] SpawnCloud(Cloud[] clouds)
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i] = pool.Pool[i].GetFreeElement();
            clouds[i].transform.position = GetRandomPosition();
        }
        return clouds;
    }
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
    private bool IsNotActive(Cloud cloud)
    {
        if (!cloud.gameObject.activeSelf) return true;
        return false;
    }
    private Vector2 GetRandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(-18f, -7f), UnityEngine.Random.Range(5f, 1f));
    }
}
