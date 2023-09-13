using Sound;
using UnityEngine;

public class YellowChickenSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;
    [SerializeField] private YellowChickenPool pool;

    public YellowChicken SpawnChicken()
    {
        YellowChicken yellowChicken = pool.Pool.GetFreeElement();
        yellowChicken.transform.position = spawnPoint.position;
        yellowChicken.Construct(soundSystem,fxSystem);
        return yellowChicken;
    }
}
