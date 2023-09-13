using Sound;
using UnityEngine;

public class Xylophone : MonoBehaviour
{
    private Animator animator;
    private SoundSystem soundSystem;
    private FxSystem fxSystem;

    public string[] notes;
    private void Start()
    {
        animator = GetComponent<Animator>();
        soundSystem = SoundSystemUser.Instance;
        fxSystem = FxSystem.Instance;
    }
    public void PlayNote(string name)
    {
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
        fxSystem.PlayEffect("NoteEffect", gameObject.transform.position, transform);
    }
    public void PlayNoteXylophone(string name)
    {
        PlayNote(name);
        Invoke("SpawnNote", 0.2f);
    }
    public void PlayNoteXyloHold(string name)
    {
        if (Input.GetMouseButton(0))
        {
            PlayNote(name);
            Invoke("SpawnNote", 0.2f);
        }
    }
}
