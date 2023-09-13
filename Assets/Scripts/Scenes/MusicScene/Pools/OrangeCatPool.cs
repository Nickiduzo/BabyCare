using Optimization;
using UnityEngine;

public class OrangeCatPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<OrangeCat> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<OrangeCat>(config.OrangeCat, 1, true, true, transform);
    }
}
