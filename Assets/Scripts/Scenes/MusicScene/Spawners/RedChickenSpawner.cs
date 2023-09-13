using Sound;
using UnityEngine;

public class RedChickenSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private RedChickenPool pool;

    public RedChicken SpawnRedChicken()
    {
        RedChicken redChicken = pool.Pool.GetFreeElement();
        redChicken.transform.position = spawnPoint.position;
        redChicken.Construct(soundSystem, fxSystem);
        return redChicken;
    }
}
