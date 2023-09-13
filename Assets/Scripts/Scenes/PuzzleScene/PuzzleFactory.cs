using System;
using Puzzle;
using UnityEngine;

public class PuzzleFactory : MonoBehaviour
{
    [SerializeField] private Transform _createPoint;
    public bool CanCreate { get; private set; } = true;

    public void Create(PuzzleController puzzle)
    {
        if (puzzle == null)
            throw new ArgumentNullException(nameof(puzzle));
        if (!CanCreate)
            throw new InvalidOperationException(nameof(Create));

        Instantiate(puzzle, _createPoint.position, _createPoint.rotation, _createPoint);
        CanCreate = false;
    }
}