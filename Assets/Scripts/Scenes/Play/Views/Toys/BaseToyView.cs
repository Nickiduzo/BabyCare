using DG.Tweening;
using Inputs;
using PlayScene;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UsefulComponents;

public abstract class BaseToyView : MonoBehaviour, IHintable
{
    [SerializeField] private Toys _kindOfToy;
    [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private MouseTrigger _mouse;

    public Toys KindOfToy => _kindOfToy;
    public Transform ParentPart { get; set; }
    public MoveToDestinationOnDragEnd DestinationOnDragEnd => _destinationOnDragEnd;
    public ChildView Child { get; set; }

    public PlaySceneMediator mediator { get; set; }
    public InputSystem InputSystem { get; set; }
    public DragAndDrop DragAndDrop { get; set; }
    public Vector3 Destination { get; set; }

    public abstract Task OnAnimStart();
    public abstract void OnAnimEnd();

    protected void Start()
    {
        Destination = transform.position;

        DragAndDrop.Construct(InputSystem);
        DestinationOnDragEnd.Construct(Destination);
        DestinationOnDragEnd.MoveToDestination();
    }

    public void MakeInteractable()
    {
        if (DragAndDrop != null)
        {
            DragAndDrop.IsDraggable = true;
            DragAndDrop.enabled = true;
        }
    }

    public void MakeNonInteractable()
    {
        DOTween.Kill(gameObject);
       
        if (DragAndDrop != null) 
        {
            DragAndDrop.IsDraggable = false;
            DragAndDrop.enabled = false;
        }
    }

    public void MakeAllInteractable()
    {
        mediator.MakeAllInteractable();
    }

    public void MakeAllNonInteractable()
    {
        mediator.MakeAllNonInteractable();
    }

    public virtual void ActivateHint() => HintController.Instance.IsActive = true;

    public virtual void DeactivateHint()
    {
        HintSystem.Instance.HidePointerHint();
        HintController.Instance.IsActive = false;
    }
}
