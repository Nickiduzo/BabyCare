using System.Linq;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class MiniSceneSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private GameObject[] huds;

    [SerializeField] private GameObject[] animals;
    [SerializeField] private UnityEngine.UI.Image[] noteButtons;
    [SerializeField] private Sprite defaultSet;
    private void Start()
    {
        HidePianoPlayer();
    }
    public void AnimalMiniScene()
    {
        TriggersOff();
        ShowAnimals();
        foreach (var backgrond in backgrounds)
        {
            if (backgrond.name == "PetsBackground")
            {
                backgrond.SetActive(true);
            }
            else
            {
                backgrond.SetActive(false);
            }
        }

        foreach (var hud in huds)
        {
            if (hud.name == "AnimalHud")
            {
                hud.SetActive(true);
            }
            else
            {
                hud.SetActive(false);
            }
        }
        HidePianoPlayer();
    }
    public void BarabanMiniScene()
    {
        TriggersOff();
        HideAnimals();
        foreach (var backgrond in backgrounds)
        {
            if (backgrond.name == "BarabanBackground")
            {
                backgrond.SetActive(true);
            }
            else
            {
                backgrond.SetActive(false);
            }
        }

        foreach (var hud in huds)
        {
            if (hud.name == "BarabanHud")
            {
                hud.SetActive(true);
            }
            else
            {
                hud.SetActive(false);
            }
        }
        ShowPianoPlayer();
    }
    public void XylophoneMiniScene()
    {
        TriggersOff();
        HideAnimals();
        foreach (var backgrond in backgrounds)
        {
            if (backgrond.name == "XylophoneBackground")
            {
                backgrond.SetActive(true);
            }
            else
            {
                backgrond.SetActive(false);
            }
        }

        foreach (var hud in huds)
        {
            if (hud.name == "XylopohoneHud")
            {
                hud.SetActive(true);
            }
            else
            {
                hud.SetActive(false);
            }
        }
        HidePianoPlayer();
    }
    public void PianoMiniScene()
    {
        TriggersOff();
        HideAnimals();
        foreach (var backgrond in backgrounds)
        {
            if (backgrond.name == "PianoBackground")
            {
                backgrond.SetActive(true);
            }
            else
            {
                backgrond.SetActive(false);
            }
        }

        foreach (var hud in huds)
        {
            if (hud.name == "PianoHud")
            {
                hud.SetActive(true);
            }
            else
            {
                hud.SetActive(false);
            }
        }
        ShowPianoPlayer();
    }

    private void HideAll()
    {
        foreach (var animal in animals)
            animal.SetActive(false);
    }
    private void HideAnimals()
    {
        foreach (var animal in animals)
        {
            if (animal.name != "PianoPlayerSpawner")
                animal.SetActive(false);
        }
    }
    private void ShowAnimals()
    {
        foreach (var animal in animals)
        {
            if (animal.name != "PianoPlayerSpawner")
                animal.SetActive(true);
        }
    }
    private void ShowPianoPlayer()
    {
        animals.FirstOrDefault(obj => obj.name == "PianoPlayerSpawner").SetActive(true);

        animals.FirstOrDefault(obj => obj.name == "PianoPlayerSpawner").
            transform.DOMove(new Vector3(0f, -6f, 0f), 1f * 0.3f).From(new Vector3(0f,0.5f,0f));
    }
    private void HidePianoPlayer()
    {
        animals.FirstOrDefault(obj => obj.name == "PianoPlayerSpawner").SetActive(false);
    }
    public void ButtonBack()
    {
        HideAll();
    }
    public void TriggersOff()
    {
        AppearNoteButton.isStopping = false;
        AppearNoteButton.isClicked = false;
        for (int i = 0; i < 3; i++)
        {
            noteButtons[i].sprite = defaultSet;
        }
    }
}
