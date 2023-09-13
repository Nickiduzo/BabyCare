using UnityEngine;
using UnityEngine.UI;

public class PuzzleSliderFactory : MonoBehaviour
{
    [SerializeField] private LayoutElementWithScaling[] _puzzleButtons;
    [SerializeField] private Button _right;
    [SerializeField] private Button _left;
    [SerializeField] private float _slideMotion;
    [SerializeField] private int _currentButton;

    public void Create()
    {
        var slider = new LayoutSlider(_puzzleButtons, _slideMotion, new Vector3(1, 0), _currentButton);

        _right.onClick.AddListener(slider.TrySlideRight);
        _left.onClick.AddListener(slider.TrySlideLeft);
    }
}