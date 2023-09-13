using Sound;
using UnityEngine;

public class HourseSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundSystem soundSystem;
    [SerializeField] private FxSystem fxSystem;

    [SerializeField] private HoursePool pool;

    public Hourse SpawnHourse()
    {
        Hourse hourse = pool.Pool.GetFreeElement();
        hourse.transform.position = spawnPoint.position;
        hourse.Construct(soundSystem, fxSystem);
        return hourse;
    }
}
