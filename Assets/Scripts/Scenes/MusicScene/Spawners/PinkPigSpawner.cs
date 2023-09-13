using Sound;
using UnityEngine;

public class PinkPigSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;
    [SerializeField] private PinkPigPool pool;
    public PinkPig SpawnPinkPig()
    {
        PinkPig pinkPig = pool.Pool.GetFreeElement();
        pinkPig.transform.position = spawnPoint.position;
        pinkPig.Construct(soundSystem, fxSystem);
        return pinkPig;
    }
}
