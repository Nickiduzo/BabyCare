using System;
using System.Collections;
using Sound;
using UnityEngine;

namespace Dream
{
    [RequireComponent(typeof(Animator), typeof(MouseTrigger))]
    public class Baby : MonoBehaviour
    {
        public event Action OnSunRise;
        public event Action OnLvlEnd;

        private SoundSystem _soundSystem;
        private Animator _animator;
        private MouseTrigger _mouseTrigger;

        private bool _lightIsOff;
        private bool _blanketOnBaby;
        private bool _toyWheelIsPlaying;

        //Script for baby behaviour

        public void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
            _animator = GetComponent<Animator>();
            _mouseTrigger = GetComponent<MouseTrigger>();
        }

        //Starts coroutine to toggle happy animation
        private void ToggleHappyAnim()
        {
            StartCoroutine(ToggleHappyAnimRoutine());
        }

        //Playing happy animation. Making baby non interactable during anim
        private IEnumerator ToggleHappyAnimRoutine()
        {
            _mouseTrigger.OnUp -= ToggleHappyAnim;
            _animator.SetTrigger("Happy");
            _soundSystem.PlaySound("FallsAsleep");
            _animator.SetTrigger("StopHappy");
            yield return new WaitForSeconds(5);
            _mouseTrigger.OnUp += ToggleHappyAnim;
        }

        //Checking the status of the gameplay objects. If light is off[_lightIsOff],
        //blanket on baby[_blanket] and toy wheel played at least once[_toyWheelIsPlaying],
        //baby should fall asleep
        public void CheckStates()
        {
            if (_lightIsOff && _blanketOnBaby && _toyWheelIsPlaying)
            {
                _animator.SetTrigger("StopHappy");
                FallAsleep();
            }
        }

        //Starting the coroutine [SleepingRoutine]
        private void FallAsleep()
        {
            StartCoroutine("SleepingRoutine");
        }

        //Wakes up a baby
        public void WakeUp()
        {
            _soundSystem.PlaySound("FallsAsleep");
            _animator.SetTrigger("Awake");
        }

        //Ends level when baby awake. Invokes in Animator, animation "IdleAwake"
        public void EndLvl()
        {
            OnLvlEnd?.Invoke();
        }

        //Make baby sleep
        private IEnumerator SleepingRoutine()
        {
            _mouseTrigger.OnUp -= ToggleHappyAnim;
            _soundSystem.PlaySound("Sleep");
            _animator.SetTrigger("Sleep");
            yield return new WaitForSeconds(17);
            OnSunRise?.Invoke();
            _soundSystem.PlaySound("Dawn");
            yield return new WaitForSeconds(4);
            WakeUp();
            _soundSystem.StopSound("Sleep");
        }

        //Invoking in [Lamp], when player press on lamp
        public void ChangeLight()
        {
            _lightIsOff = true;
        }

        //Invoking in [Blanket], when player put blanket on baby
        public void ChangeBlanket()
        {
            _blanketOnBaby = true;
            _animator.SetBool("IsCold", false);
            _mouseTrigger.OnUp += ToggleHappyAnim;
        }

        //Invoking in [ToyWheel], when player press on toy wheel
        public void ChangeToyWheel()
        {
            _toyWheelIsPlaying = true;
        }
    }
}
