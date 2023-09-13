using Sound;
using UnityEngine;

public class WhiteSheepSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private WhiteSheepPool pool;

    public WhiteSheep SpawnSheep()
    {
        WhiteSheep whiteSheep = pool.Pool.GetFreeElement();
        whiteSheep.transform.position = spawnPoint.position;
        whiteSheep.Construct(soundSystem, fxSystem);
        return whiteSheep;
    }
}
