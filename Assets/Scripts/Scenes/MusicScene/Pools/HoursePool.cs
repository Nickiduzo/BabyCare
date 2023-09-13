using Optimization;
using UnityEngine;

public class HoursePool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<Hourse> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<Hourse>(config.Hourse, 1, true, true, transform);
    }
}
