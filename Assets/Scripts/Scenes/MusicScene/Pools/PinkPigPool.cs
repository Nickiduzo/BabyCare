using Optimization;
using UnityEngine;

public class PinkPigPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<PinkPig> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<PinkPig>(config.PinkPig, 1, true, true, transform);
    }
}
