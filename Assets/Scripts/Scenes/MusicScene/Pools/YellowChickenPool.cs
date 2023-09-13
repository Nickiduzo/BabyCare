using Optimization;
using UnityEngine;

public class YellowChickenPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<YellowChicken> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<YellowChicken>(config.YellowChicken, 1, true, true, transform);
    }
}
