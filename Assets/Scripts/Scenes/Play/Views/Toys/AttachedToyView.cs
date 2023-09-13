using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UsefulComponents;

public class AttachedToyView : BaseToyView, IAttachable
{
    public string KindOfObject => KindOfToy.ToString();
    public bool IsAttachable => true;

    public DragAndDrop dragAndDrop => DragAndDrop;

    private new void Start()
    {
        base.Start();
        DragAndDrop.OnDragStart += DeactivateHint;
        DragAndDrop.OnDragEnded += MakeAllInteractable;
    }
    public override async Task OnAnimStart()
    {
        MakeNonInteractable();
        MakeAllNonInteractable();
        await ToyService.SetAndMoveToParentPosition(this, ParentPart);
        ToyService.SetLocalPosition(this);

        ToyService.IsPlaying = true;
    }

    public override void OnAnimEnd()
    {
        transform.parent = null;
        MakeInteractable();
        MakeAllInteractable();
        DestinationOnDragEnd.MoveToDestination();

        ToyService.IsPlaying = false;
    }
}
