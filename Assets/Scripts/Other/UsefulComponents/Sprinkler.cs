using System;
using Sound;
using UnityEngine;
using UsefulComponents;

public class Sprinkler : MonoBehaviour
{
    [SerializeField] private bool DontDestroyOnLoad;

    private Action OnTriggerEnterSub;
    private Action OnTriggerStaySub;
    private Action OnTriggerEndSub;
    private Action OnTriggerEnterUnsub;
    private Action OnTriggerStayUnsub;
    private Action OnTriggerEndUnsub;

    private Action StoppedFlow;
    private Action StartedFlow;
    private DragAndDrop _dragAndDrop;
    private Animator _animator;
  
    private Action StartDragCarrot;
    private BaseHoleTriggerObserver _baseHoleTriggerObserverCarrot;
    private Collider2D _colliderCarrotStream;
    private SpriteRenderer _spriteRendererCarrotStream;
    
    
    #region Singleton

    public static Sprinkler Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        if (DontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion Singleton

    private void OnDestroy()
    {
        OnTriggerEnterUnsub?.Invoke();
        OnTriggerStayUnsub?.Invoke();
        OnTriggerEndUnsub?.Invoke();
        ClearAllUnsub();
        OnDragAndDropDisable();
        OnCarrotPumpStreamDisable();
    }

    #region Carrot realization

    public void InitCarrot(DragAndDrop _dragAndDrop)
    {
       
        this._dragAndDrop = _dragAndDrop;

        CarrotSubAction();
    }

    public void InitCarrotStream(BaseHoleTriggerObserver _baseHoleTriggerObserver, Collider2D _collider, SpriteRenderer _sprite)
    {
        Debug.Log("init fucking Carrot");
        _baseHoleTriggerObserverCarrot = _baseHoleTriggerObserver;
        _colliderCarrotStream = _collider;
        _spriteRendererCarrotStream = _sprite;
        StartDragCarrot?.Invoke();
        
        CarrotStreamSubAction();
    }

    private void CarrotSubAction()
    {
        SetActionsToDragAndDrop(onDragStart: EnableWaterCarrot, onDragEnd: DisableWaterCarrot);
    }

    private void CarrotStreamSubAction()
    {
        _baseHoleTriggerObserverCarrot.OnTriggerStay += PureHole;
    }

    private void OnCarrotPumpStreamDisable()
    {
        if (_baseHoleTriggerObserverCarrot != null)
        {
            _baseHoleTriggerObserverCarrot.OnTriggerStay -= PureHole;
        }
    }
    
    private void EnableWaterCarrot()
    {
        EnableWater(_colliderCarrotStream, _spriteRendererCarrotStream);
    }

    private void DisableWaterCarrot()
    {
        DisableWater(_colliderCarrotStream, _spriteRendererCarrotStream);
    }

    #endregion Carrot realization

    #region Regular Functions

    private void EnableWater(Collider2D _collider, SpriteRenderer _spriteRenderer)
    {

        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        StartSound();
    }

    private void DisableWater(Collider2D _collider, SpriteRenderer _spriteRenderer)
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        StopSound();
    }

    private void SetActionsToDragAndDrop(Action onDragStart = null, Action onDragStay = null, 
        Action onDragEnd = null)
    {
        _dragAndDrop.OnDragStart += onDragStart;
        _dragAndDrop.OnDrag += onDragStay;
        _dragAndDrop.OnDragEnded += onDragEnd;
    }

    #endregion Regular Functions
    
    #region Trivial Functions

    private void ClearAllSub()
    {
        OnTriggerEnterSub = null;
        OnTriggerStaySub = null;
        OnTriggerEndSub = null;
    }
    
    private void ClearAllUnsub()
    {
        OnTriggerEnterUnsub = null;
        OnTriggerStayUnsub = null;
        OnTriggerEndUnsub = null;
    }
    
    private void PureHole(BaseHole hole)
        => hole.AddProgress(Time.deltaTime * 5);
    
    private void StartSound() => SoundSystemUser.Instance.PlaySound("Water");
        
    private void StopSound() => SoundSystemUser.Instance.StopSound("Water");
    
    private void DeactivateHint() => HintSystem.Instance.HidePointerHint();

    private void OnDragAndDropDisable()
    {
        if (_dragAndDrop != null) _dragAndDrop = null;
    }

    #endregion Trivial Functions
}
