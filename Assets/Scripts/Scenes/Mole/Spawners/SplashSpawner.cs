using Mole.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSpawner : MonoBehaviour
{
    [SerializeField] HoleablePoolManager _pool;
    [SerializeField, Range(0, 1f)] float _maxDelay = .8f;
    [SerializeField, Range(0, .5f)] float _minDelay = .1f;
    [SerializeField, Range(1f, 2f)] float _scaleMax = 2f;
    [SerializeField, Range(1f, 360f)] float _rotationMax = 360f;
    [SerializeField, Range(-5f, 5f)] float _yPosMax, _yPosMin;
    [SerializeField, Range(-10f, 10f)] float _xPosMax, _xPosMin;

    #region Singletone
    
    public static SplashSpawner Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    #endregion

    public void SpawnSplash(int count = 1)
    {
        StartCoroutine(Spawn(count));
    }

    IEnumerator Spawn(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var splash = _pool.SplashPool.GetFreeElement();

            var scale = Random.Range(1.5f, _scaleMax);
            splash.transform.localScale = new Vector3(scale, scale, scale);

            splash.transform.SetPositionAndRotation(new Vector2(Random.Range(_xPosMin, _xPosMax), Random.Range(_yPosMin, _yPosMax)),
                Quaternion.Euler(0, 0, Random.Range(0f, _rotationMax)));

            splash.gameObject.SetActive(true);

            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        }
    }
}
