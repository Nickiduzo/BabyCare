using Sound;
using UI;
using UnityEngine;

public class LeftDrum : HudElement
{
    private SoundSystem soundSystem;
    private Animator animator;
    private FxSystem fxSystem;

    public static bool isClicked = false;
    private new void Start()
    {
        base.Start();
        Appear();
        animator = GetComponent<Animator>();
        soundSystem = SoundSystemUser.Instance;
        fxSystem = FxSystem.Instance;
    }

    public void PlateAnimSound()
    {
        isClicked = true;
        animator.SetTrigger("isLeftPlate");
        soundSystem.PlaySound("LeftPlate");
    }

    public void DrumAnimSound()
    {
        isClicked = true;
        animator.SetTrigger("isLeftDrum");
        soundSystem.PlaySound("LeftBaraban");
    }
    public void PlayEffect(GameObject note)
    {
        fxSystem.PlayEffect("StarEffect", note.transform.position, note.transform);
        Invoke("SpawnNote", 0.2f);
    }
    public void SpawnNote()
    {
        fxSystem.PlayEffect("NoteEffect", gameObject.transform.position, transform);
        isClicked = false;
    }
    //Special methods for AppearNoteButton 
    public void PlayDrumNote()
    {
        animator.SetTrigger("isLeftDrum");
        soundSystem.PlaySound("LeftBaraban");
        Invoke("SpawnNote", 0f);
    }
    public void PlayPlateNote()
    {
        animator.SetTrigger("isLeftPlate");
        soundSystem.PlaySound("LeftPlate");
        Invoke("SpawnNote", 0f);
    }
}
