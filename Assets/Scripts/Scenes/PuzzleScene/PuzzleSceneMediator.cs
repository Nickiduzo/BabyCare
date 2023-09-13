using UnityEngine;
using UnityEngine.UI;

public class PuzzleSceneMediator : MonoBehaviour
{
    [SerializeField] private PuzzleSliderFactory _sliderFactory;
    [SerializeField] private Button[] _puzzleButtons;
    [SerializeField] private Animator _sliderAnimator;
    [SerializeField] private string _hideAnimation;

    private void Awake() =>
        _sliderFactory.Create();

    private void OnEnable()
    {
        foreach (var puzzleButton in _puzzleButtons) 
            puzzleButton.onClick.AddListener(HideChoseUi);
    }

    private void OnDisable()
    {
        foreach (var puzzleButton in _puzzleButtons) 
            puzzleButton.onClick.RemoveListener(HideChoseUi);
    }

    private void HideChoseUi() => 
        _sliderAnimator.Play(_hideAnimation);
}