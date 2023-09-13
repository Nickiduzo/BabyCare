using Sound;
using UnityEngine;

public class PuppySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private PuppyPool pool;

    public Puppy SpawnPuppy()
    {
        Puppy puppy = pool.Pool.GetFreeElement();
        puppy.transform.position = spawnPoint.position;
        puppy.Construct(soundSystem, fxSystem);
        return puppy;
    }
}
