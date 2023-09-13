using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FishBody : MonoBehaviour
{
    [field:SerializeField] public List<SpriteRenderer> SpriteRenderers { get; private set; }
    public Animator Animator { get; private set; }
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void ChangeSpriteSortOrder(int SortOrderIndex)
    {
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers)
        {
            spriteRenderer.sortingOrder += SortOrderIndex;
        }
    }
}