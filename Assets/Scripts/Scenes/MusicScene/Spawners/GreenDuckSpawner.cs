using Sound;
using UnityEngine;

public class GreenDuckSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private GreenDuckPool pool;

    public GreenDuck SpawnGreenDuck()
    {
        GreenDuck greenDuck = pool.Pool.GetFreeElement();
        greenDuck.transform.position = spawnPoint.position;
        greenDuck.Construct(soundSystem, fxSystem);
        return greenDuck;
    }
}
