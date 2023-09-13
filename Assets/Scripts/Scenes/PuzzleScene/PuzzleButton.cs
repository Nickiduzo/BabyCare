using System;
using Puzzle;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PuzzleButton : MonoBehaviour
{
    [SerializeField] private PuzzleController _puzzle;
    [SerializeField] private PuzzleFactory _factory;

    private void Awake()
    {
        if (_factory == null)
            throw new NullReferenceException(nameof(_factory));

        var button = GetComponent<Button>();
        button.onClick.AddListener(Spawn);
    }

    private void Spawn()
    {
        if (_factory.CanCreate)
            _factory.Create(_puzzle);
    }
}