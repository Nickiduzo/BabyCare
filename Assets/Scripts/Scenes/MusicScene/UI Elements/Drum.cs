using Sound;
using UI;
using UnityEngine;

public class Drum : HudElement
{
    private Animator animator;
    private SoundSystem soundSystem;
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
    public void PlayDrum()
    {
        isClicked = true;
        animator.SetTrigger("isDrum");
        soundSystem.PlaySound("BigBaraban");
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
    public void PlayNote()
    {
        animator.SetTrigger("isDrum");
        soundSystem.PlaySound("BigBaraban");
        Invoke("SpawnNote", 0f);
    }
}
