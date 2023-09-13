using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSplash : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] List<Sprite> _sprites; 

    private void Start()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }

    public void OnAnimEnd() 
        => gameObject.SetActive(false);
}
