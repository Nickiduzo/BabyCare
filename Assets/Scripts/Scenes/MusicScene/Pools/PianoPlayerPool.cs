using Optimization;
using UnityEngine;

public class PianoPlayerPool : MonoBehaviour
{
    [SerializeField] private MiniMusicConfig config;

    public PoolMono<PianoPlayer> Pool { get; private set; }

    private void Awake()
    {
        Pool = new PoolMono<PianoPlayer>(config.PianoPlayer, 1, true, true, transform);
    }
}
