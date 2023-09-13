using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkView : MonoBehaviour
{
    [SerializeField] Transform _begin;
    [SerializeField] Transform _end;
    [SerializeField] AnimationCurve _chanceFromDistance;


    public Transform Begin => _begin; //Start of chunk (set on each chunk)
    public Transform End => _end; //End of chunk (set on each chunk)
    public AnimationCurve ChanceFromDistance => _chanceFromDistance;
}
