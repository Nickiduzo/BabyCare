using System.Collections;
using System.Diagnostics.Tracing;
using System.Linq;
using Scene;
using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private PuzzlePiece[] _pieces;
        [SerializeField] private Animator _puzzleAnimator;
        [SerializeField] private Animator _modelAnimator;
        [SerializeField] private GameObject _puzzle;
        [SerializeField] private GameObject _model;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private string _startSound;
        [SerializeField] private string _completeSound;
        [SerializeField] private SceneLoader _sceneLoader;

        private UnityEvent _event = new UnityEvent();

        private string _triggerToSplit = "Split";

        private void Start()
        {
            StartCoroutine(StartPuzzle());
            _event.AddListener(EndPuzzle);
        }

        private void Update()
        {
            if (IsPuzzleCollected()) 
                _event.Invoke();
        }

        private void EndPuzzle()
        {
            _event = null;
            StartCoroutine(EndPuzzleCoroutine());
        }

        IEnumerator EndPuzzleCoroutine()
        {
            yield return new WaitForSeconds(2);

            _puzzle.SetActive(false);
            _model.SetActive(true);
            _soundSystem.PlaySound(_completeSound);

            PuzzleParticleController.Instance.PlayCollectedPuzzle();

            yield return new WaitForSeconds(2);

            _sceneLoader.ReloadCurrentScene();
        }

        private bool IsPuzzleCollected()
        {
            return _pieces.All(piece => piece.IsCollected);
        }

        private IEnumerator StartPuzzle()
        {
            _puzzleAnimator.SetTrigger(_triggerToSplit);
            _soundSystem.PlaySound(_startSound);

            yield return new WaitUntil(() => AnimationChecker.Instance.IsAnimationOver(_puzzleAnimator, "Split"));

            _puzzleAnimator.enabled = false;

            foreach (var piece in _pieces)
                piece.enabled = true;
        }
    }
}