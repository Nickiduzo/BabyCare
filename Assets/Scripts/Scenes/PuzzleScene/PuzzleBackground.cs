using UnityEngine;
using UnityEngine.UI;

public class PuzzleBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private Sprite[] _spritesToSet;
    private Transform[] _images;
    
    private void OnEnable()
    {
        _images = new Transform[transform.childCount];

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        UpdateBackgroundSprites();
    }

    private void UpdateBackgroundSprites()
    {
        for (int i = 0; i < _images.Length; i++)
        {
            float rotation = Random.Range(0, 360);
            int random = Random.Range(0, _sprites.Length);

            _images[i].GetComponent<Image>().sprite = _sprites[random];
            _images[i].Rotate(0.0f, 0.0f, rotation, Space.World);
        }
    }
}
