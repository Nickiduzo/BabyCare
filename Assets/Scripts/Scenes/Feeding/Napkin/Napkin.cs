using Inputs;
using Sound;
using UnityEngine;
using UsefulComponents;

namespace Feeding
{
    public class Napkin : MonoBehaviour
    {
        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] private DragAndDrop _dragAndDrop;

        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        public DragAndDrop DragAndDrop => _dragAndDrop;

        public void Awake()
        {
            Construct(_inputSystem, _soundSystem);
            BacktoBox();
        }

        //when construct Napkin gameObject
        public void Construct(InputSystem inputSystem, SoundSystem soundSystem)
        {
            _inputSystem = inputSystem;
            _soundSystem = soundSystem;

            _dragAndDrop.Construct(inputSystem);

            _dragAndDrop.OnDragStart += WipingSound;
            //_dragAndDrop.OnDragEnded -= WipingSound;

            _destinationOnDragEnd.Construct(transform.position);
            _destinationOnDragEnd.OnMoveComplete += BacktoBox;
            DragAndDrop.OnDragStart += TurnOnNapkin;

        }

        private void WipingSound()
        {
            _soundSystem.PlaySound("napkin");
        }

        private void BacktoBox()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        private void TurnOnNapkin()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

        //disable hint when player tap Napkin
        private void OnMouseDown()
        {
            HintSystem.Instance.HidePointerHint();
        }
    }
}