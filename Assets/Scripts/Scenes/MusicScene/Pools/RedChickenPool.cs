using Optimization;
using UnityEngine;

public class RedChickenPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<RedChicken> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<RedChicken>(config.RedChicken, 1, true, true, transform);
    }
}
