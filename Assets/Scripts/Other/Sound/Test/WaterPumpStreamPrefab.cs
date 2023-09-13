using Sound;
using UnityEngine;

public class WaterPumpStreamPrefab : MonoBehaviour
{
    private const string WATER_SOUND_NAME = "Water";

    [SerializeField] private BaseHoleTriggerObserver _observer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private ISoundSystem _soundSystem;
    private bool _isWorking;

    public void Construct(SoundSystem soundSystem)
    {
        _soundSystem = soundSystem;
    }
    private void Awake()
    {
        _observer.OnTriggerStay += PureHole;
    }

    private void OnDestroy()
    {
        _observer.OnTriggerStay -= PureHole;
    }

    private void PureHole(BaseHole hole)
    => hole.AddProgress(Time.deltaTime * 5);

    public bool IsWorking()
    {
        return _isWorking;
    }
    public void EnableWater()
    {
        _isWorking = true;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        _soundSystem.PlaySound(WATER_SOUND_NAME);
    }

    public void EnableWaterInTomato()
    {
        _spriteRenderer.enabled = true;
        _soundSystem.PlaySound(WATER_SOUND_NAME);
    }

    public void DisableWaterInTomato()
    {
        _spriteRenderer.enabled = false;
        _soundSystem.StopSound(WATER_SOUND_NAME);
    }

    public void DisableWater()
    {
        _isWorking = false;
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _soundSystem.StopSound(WATER_SOUND_NAME);
    }

    public void RestoreCollider()
    {
        _collider.enabled = true;
    }

    public void EnableCollider(bool enabled)
    {
        _collider.enabled = enabled;
    }
}
