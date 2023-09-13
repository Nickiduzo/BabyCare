using Sound;
using UnityEngine;

public class PianoPlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;
    [SerializeField] private PianoPlayerPool pool;

    public PianoPlayer SpawnPlayer()
    {
        PianoPlayer player = pool.Pool.GetFreeElement();
        player.transform.position = spawnPoint.position;
        player.Constructor(soundSystem,fxSystem);
        return player;
    }
}
