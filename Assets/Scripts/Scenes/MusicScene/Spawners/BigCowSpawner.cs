using Sound;
using UnityEngine;

public class BigCowSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private BigCowPool pool;

    public BigCow SpawnBigCow()
    {
        BigCow bigCow = pool.Pool.GetFreeElement();
        bigCow.transform.position = spawnPoint.position;
        bigCow.Construct(soundSystem,fxSystem);
        return bigCow;
    }
}
