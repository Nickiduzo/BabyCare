using Optimization;
using UnityEngine;

public class GreenDuckPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<GreenDuck> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<GreenDuck>(config.GreenDuck, 1, true, true, transform);
    }
}
