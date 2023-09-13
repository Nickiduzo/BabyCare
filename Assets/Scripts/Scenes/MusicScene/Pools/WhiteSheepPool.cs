using Optimization;
using UnityEngine;

public class WhiteSheepPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<WhiteSheep> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<WhiteSheep>(config.WhiteSheep, 1, true, true, transform);
    }
}
