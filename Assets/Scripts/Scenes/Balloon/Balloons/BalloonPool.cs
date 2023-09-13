using Optimization;
using UnityEngine;

public class BalloonPool : MonoBehaviour
{
    [SerializeField] private BalloonConfig config;

    public PoolMono<Balloon>[] Pool { get; set; }

    private void Awake()
    {
        Pool = new PoolMono<Balloon>[config.Balloon.Length];
        for (int i = 0; i < config.Balloon.Length; i++)
        {
            Pool[i] = new PoolMono<Balloon>(config.Balloon[i],1,true,true,transform);
        }
    }
}
