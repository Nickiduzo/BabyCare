using DG.Tweening;
using Sound;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseHoleable : MonoBehaviour
{
    public UnityEvent OnCatch { get; protected set; } = new();
    public UnityEvent<BaseHoleable> OnDied { get; protected set; } = new();
    public UnityEvent<BaseHoleable> OnHied { get; protected set; } = new();

    [SerializeField] private Collider2D _collider;
    [SerializeField] private MouseTrigger _mouseTrigger;
    [SerializeField] private float _appearDuration;
    [SerializeField] private float _appearOffsetMoving;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector2 _minMaxHideDelay;

    public bool IsRegistered { get; set; }
    public int SpawnPointIndex => _spawnPointIndex;

    protected Collider2D Collider => _collider;
    protected MouseTrigger MouseTrigger => _mouseTrigger;
    protected float AppearDuration => _appearDuration;
    protected float AppearOffsetMoving => _appearOffsetMoving;
    protected Animator Animator => _animator;
    protected Vector2 MinMaxHideDelay => _minMaxHideDelay;

    protected Vector3 scale;
    protected bool isInit;
    protected ISoundSystem soundSystem;
    protected FxSystem fxSystem;
    protected Tween hideTween;

    private int _spawnPointIndex;
    protected bool isHitting = false;

    public abstract Tween Appear();
    public abstract void Hide();
    protected abstract void Hit();

    private void Start()
        => _mouseTrigger.OnDown += HummerShow;

    private void HummerShow()
    {
        if (!isHitting)
        {
            isHitting = true;
            HummerSystem.Instance.Show(transform.position, Hit);
        }
    }

    /// <summary>
    /// Set up [soundSystem] and [fxSystem]
    /// </summary>
    /// <param name="soundSystem"></param>
    /// <param name="fxSystem"></param>
    public void Construct(ISoundSystem soundSystem, FxSystem fxSystem)
    {
        if (isInit) return;

        isInit = true;
        this.soundSystem = soundSystem;
        this.fxSystem = fxSystem;
    }

    /// <summary>
    /// Set spawn point and holeable scale
    /// </summary>
    /// <param name="spawnPointIndex"></param>
    /// <param name="scale"></param>
    public void SetSpawnPointIndexAndScale(int spawnPointIndex, float scale)
    {
        this.scale = new Vector3(scale, scale, scale);
        _spawnPointIndex = spawnPointIndex;
    }

    /// <summary>
    /// play a sound effect based on name of the sound
    /// </summary>
    /// <param name="name"></param>
    protected void PlaySound(string name)
        => soundSystem.PlaySound(name);

    /// <summary>
    /// stop a sound effect based on name of the sound
    /// </summary>
    /// <param name="name"></param>
    protected void StopSound(string name)
        => soundSystem.StopSound(name);

    /// <summary>
    /// mole move to down
    /// </summary>
    /// <returns></returns>
    protected Tween MoveOff()
        => transform.DOMoveY(transform.localPosition.y - _appearOffsetMoving * 10f, _appearDuration);

    protected virtual void ShowDieFx(string name)
        => fxSystem.PlayEffect(name, transform.position);

    /// <summary>
    /// invoke [OnDied] that mole is dead and disable it
    /// </summary>
    protected virtual void DisableObject()
    {
        gameObject.SetActive(false);
        OnDied?.Invoke(this);
    }
    protected virtual Tween Disappear()
    {
        var tween = transform.DOScale(Vector3.zero, AppearDuration);

        return tween;
    }
    /// <summary>
    /// disable eyes sprites and collider, stop idle anim
    /// </summary>
    protected virtual void HideRenderer()
    {
        //Animator.enabled = false;
        Collider.enabled = false;
    }
    /// <summary>
    /// invoke [OnHide] and disable holeable as gamobject
    /// </summary>
    protected virtual void HideObject()
    {
        HideRenderer();
        gameObject.SetActive(false);
        OnHied?.Invoke(this);
    }
}
