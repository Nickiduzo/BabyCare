using DG.Tweening;
using Sound;
using UnityEngine;

public class Hourse : MonoBehaviour
{
    private Animator animator;
    private SoundSystem soundSystem;
    private FxSystem fxSystem;

    private Vector3 initialScale = new Vector3(0, 0, 0);
    private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private float duration = 1f;

    private float timeToClick = 1f;
    private bool isClicked = false;
    public void Construct(SoundSystem soundSystem, FxSystem fxSystem)
    {
        this.soundSystem = soundSystem;
        this.fxSystem = fxSystem;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        ScaleOut();
    }
    private void Update()
    {
        if (timeToClick <= 0 && isClicked == true)
        {
            timeToClick = 2f;
            isClicked = false;
        }
        else if (timeToClick > 0)
        {
            timeToClick -= Time.deltaTime;
        }
    }
    private void ScaleOut()
    {
        transform.DOScale(targetScale, duration * 1.5f).From(initialScale).SetDelay(0.9f);
    }
    private void OnMouseDown()
    {
        if (timeToClick <= 0)
        {
            fxSystem.PlayEffect("StarEffect", gameObject.transform.position, transform);
            soundSystem.PlaySound("isDonkeySound");
            animator.SetTrigger("isDonkeyAnim");
            Invoke("SpawnNote", 0.3f);
            isClicked = true;
        }
    }
    private void SpawnNote()
    {
        fxSystem.PlayEffect("NoteEffect", gameObject.transform.position, transform);
    }
}
