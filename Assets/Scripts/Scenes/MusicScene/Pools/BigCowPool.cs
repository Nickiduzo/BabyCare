using Optimization;
using UnityEngine;

public class BigCowPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<BigCow> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<BigCow>(config.BigCow, 1, true, true, transform);
    }
}
