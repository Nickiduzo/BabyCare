using DG.Tweening;
using Sound;
using System.Diagnostics;
using UnityEngine;
//using UnityEngine.Rendering.VirtualTexturing;

public class PianoPlayer : MonoBehaviour
{
    private Animator animator;
    private SoundSystem soundSystem;
    private FxSystem fxSystem;

    private Vector3 initialPosition = new Vector3(0f,-6f, 0f);
    private Vector3 targetPosition = new Vector3(0f, 0.5f, 0f);
    private float duration = 2f;
    public void Constructor(SoundSystem soundSystem,FxSystem fxSystem)
    {
        this.soundSystem = soundSystem;
        this.fxSystem = fxSystem;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        ScaleOut();
    }
    private void ScaleOut()
    {
        transform.DOMove(targetPosition,duration * 0.5f).From(initialPosition).SetDelay(0.4f);
    }
    private void FixedUpdate()
    {
        if (RightDrum.isClicked == true)
        {
            PlayAnimSound();
            RightDrum.isClicked = false;
        }
        else if (LeftDrum.isClicked == true)
        {
            PlayAnimSound();
            LeftDrum.isClicked = false;
        }
        else if (Drum.isClicked == true)
        {
            PlayAnimSound();
            Drum.isClicked = false;
        }
        else if (Piano.isClicked == true)
        {
            PlayAnimSound();
            Piano.isClicked = false;
        }
    }
    private void PlayAnimSound()
    {
        animator.SetTrigger("isClick");
    }
    private void SpawnNote()
    {
        fxSystem.PlayEffect("NoteEffect", gameObject.transform.position, transform);
    }
}
