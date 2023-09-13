using Optimization;
using UnityEngine;

public class PuppyPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<Puppy> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<Puppy>(config.Puppy, 1, true, true, transform);
    }
}
