using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class AppearNoteButton : HudElement
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unPressedSprite;

    private UnityEngine.UI.Image mainImage;
    
    public Piano piano;
    public Xylophone xylophone;
    
    public Drum drum;
    public LeftDrum leftDrum;
    public RightDrum rightDrum;

    public static bool isClicked = false;
    public static bool isStopping = false;
    private void Start()
    {
        base.Start();
        Appear();
        mainImage = GetComponent<UnityEngine.UI.Image>();  
    }
    public void PianoNotes()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayPianoNotes());
            mainImage.sprite = pressedSprite;
        }
        else
        {
            if (!isStopping)
            {
                isStopping = true;
                mainImage.sprite = unPressedSprite;
            }
        }
    }
    public void DrumsNotes()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayDrumNotes());
            mainImage.sprite = pressedSprite;
        }
        else
        {
            if (!isStopping)
            {
                isStopping = true;
                mainImage.sprite = unPressedSprite;
            }
        }
    }
    public void XylophoneNotes()
    {
        if (!isClicked)
        {
            isClicked = true;
            StartCoroutine(PlayXylophoneNotes());
            mainImage.sprite = pressedSprite;
        }
        else
        {
            if (!isStopping)
            {
                isStopping = true;
                mainImage.sprite = unPressedSprite;
            }
        }
    }

    private IEnumerator PlayPianoNotes()
    {
        foreach (var note in piano.takeNotes)
        {
            if (isStopping)
            {
                isClicked = false;
                isStopping = false;
                yield break;
            }

            piano.PlayNote(note);
            yield return new WaitForSeconds(0.6f);
        }

        isClicked = false;
        mainImage.sprite = unPressedSprite;
    }
    private IEnumerator PlayDrumNotes()
    {
       if (isClicked && isStopping)
        {
            isClicked = false;
            isStopping = false;
            yield break;
        }

        isClicked = true; 
        drum.PlayNote();
        yield return new WaitForSeconds(0.5f);

        if (isStopping)
        {
            isClicked = false;
            isStopping = false;
            yield break;
        }

        leftDrum.PlayDrumNote();
        yield return new WaitForSeconds(0.5f);

        if (isStopping)
        {
            isClicked = false;
            isStopping = false;
            yield break;
        }

        leftDrum.PlayPlateNote();
        yield return new WaitForSeconds(0.5f);

        if (isStopping)
        {
            isClicked = false;
            isStopping = false;
            yield break;
        }

        rightDrum.PlayDrumNote();
        yield return new WaitForSeconds(0.5f);

        if (isStopping)
        {
            isClicked = false;
            isStopping = false;
            yield break;
        }

        rightDrum.PlayPlateNote();
        yield return new WaitForSeconds(0.5f);

        isClicked = false; 
        mainImage.sprite = unPressedSprite; 
    }
    private IEnumerator PlayXylophoneNotes()
    {
        foreach (var note in xylophone.notes)
        {
            if (isStopping)
            {
                isClicked = false;
                isStopping = false;
                yield break;
            }

            xylophone.PlayNoteXylophone(note);
            yield return new WaitForSeconds(0.5f);
        }
        isClicked = false;
        mainImage.sprite = unPressedSprite;
    }
}
