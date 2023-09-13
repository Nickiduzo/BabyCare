using Optimization;
using UnityEngine;

public class CloudPool : MonoBehaviour
{
    [SerializeField] private BalloonConfig balloonConfig;

    public PoolMono<Cloud>[] Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<Cloud>[balloonConfig.Clouds.Length];
        for (int i = 0; i < balloonConfig.Clouds.Length; i++)
        {
            Pool[i] = new PoolMono<Cloud>(balloonConfig.Clouds[i],1,true,true,transform);
        }
    }
}
