using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ProgressBarSeparatorsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _separator;
    private GridLayoutGroup _layoutGroup;
    private List<GameObject> _separators = new List<GameObject>();

    private void Awake()
        => _layoutGroup = GetComponent<GridLayoutGroup>();

    public void CreateSeparators(int count, float sizeX)
    {
        _layoutGroup.constraintCount = count;
        _layoutGroup.cellSize = new Vector2(sizeX, _layoutGroup.cellSize.y);
        for (int i = 0; i < count - 1; i++)
        {
            _separators.Add(Instantiate(_separator, _layoutGroup.gameObject.transform));
        }
    }
    
    public void ResetSeparators()
    {
        foreach (var separator in _separators)
        {
            Destroy(separator);
        }
        _separators.Clear();
    }

}
