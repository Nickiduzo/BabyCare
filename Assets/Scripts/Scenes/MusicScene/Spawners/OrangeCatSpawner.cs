using Sound;
using UnityEngine;

public class OrangeCatSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private OrangeCatPool pool;

    public OrangeCat SpawnCat()
    {
        OrangeCat orangeCat = pool.Pool.GetFreeElement();
        orangeCat.transform.position = spawnPoint.position;
        orangeCat.Construct(soundSystem, fxSystem);
        return orangeCat;
    }
}
