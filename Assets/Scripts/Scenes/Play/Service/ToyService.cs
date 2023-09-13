using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class ToyService
{
    /// <summary>
    /// Does the toy animation lose now
    /// </summary>
    public static bool IsPlaying { get; set; } = false;

    /// <summary>
    /// Places the object in zero local coordinates
    /// </summary>
    /// <param name="toy">The object to insert</param>
    public static void SetLocalPosition(BaseToyView toy)
    {
        toy.transform.localPosition = new Vector3(0, 0, 10);
        toy.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Sets the object's parent and smoothly moves it to zero local coordinates
    /// </summary>
    /// <param name="toy">Object to move</param>
    /// <param name="parent">Parent</param>
    /// <returns>Task when the move is complete</returns>
    public static async Task SetAndMoveToParentPosition(BaseToyView toy, Transform parent)
    {
        toy.transform.SetParent(parent);

        var tcs = new TaskCompletionSource<bool>();
        var sequence = DOTween.Sequence();
        sequence.Append(toy.transform.DOLocalMove(new Vector3(0 ,0,10), toy.DestinationOnDragEnd.Duration));
        sequence.OnComplete(() =>
        {
            tcs.SetResult(true); // Set the task as completed
        });
        sequence.Play();

        await tcs.Task;
    }
}
