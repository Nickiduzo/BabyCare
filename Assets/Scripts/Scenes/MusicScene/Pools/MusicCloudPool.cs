using Optimization;
using UnityEngine;

public class MusicCloudPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<Cloud>[] Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<Cloud>[config.Clouds.Length];
        for (int i = 0; i < config.Clouds.Length; i++)
        {
            Pool[i] = new PoolMono<Cloud>(config.Clouds[i], 1, true, true, transform);
        }
    }
}
