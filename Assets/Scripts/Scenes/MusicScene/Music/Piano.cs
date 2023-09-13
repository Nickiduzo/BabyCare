using Sound;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piano : MonoBehaviour
{
    private Animator animator;
    private SoundSystem soundSystem;
    private FxSystem fxSystem;

    public static bool isClicked = false;
    public bool isPressed = false;
    public string[] takeNotes;
    private void Start()
    {
        animator = GetComponent<Animator>();
        soundSystem = SoundSystemUser.Instance;
        fxSystem = FxSystem.Instance;
    }
    public void PlayAnimSound(string name)
    {
        isClicked = true;
        animator.SetTrigger(name);
        soundSystem.PlaySound(name);
    }
    public void PlayEffect(GameObject note)
    {
        fxSystem.PlayEffect("StarEffect", note.transform.position, note.transform);
        Invoke("SpawnNote", 0.2f);
    }
    private void SpawnNote()
    {
        isClicked = false;
        fxSystem.PlayEffect("NoteEffect", gameObject.transform.position, transform);
    }
    public void PlayNote(string name)
    {
        isClicked = true;
        animator.SetTrigger(name);
        soundSystem.PlaySound(name);
        Invoke("SpawnNote",0);
    }
    public void PlayHoldNote(string name)
    {
        if (Input.GetMouseButton(0))
        {
            isClicked = true;
            animator.SetTrigger(name);
            soundSystem.PlaySound(name);
            Invoke("SpawnNote", 0);
        }
    }
}
