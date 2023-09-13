using Quest;
using Scene;
using System.Collections;
using UI;
using UnityEngine;

public class PlayButton : HudElement
{
    [SerializeField] private ChooseBaby _chooseBaby;

    /// <summary>
    /// Показує кнопку для початку гри
    /// </summary>
    private new void Start()
    {
        base.Start();
        _soundSystem.PlaySound("BackSounds");
        Appear();
    }

    /// <summary>
    /// Запускає процес переходу у сцену "BabyChoice"
    /// </summary>
    public void GoToBabyChoice()
    {
        _soundSystem.PlaySound("PlaybuttonUIClick");
        Disappear();
        this.gameObject.SetActive(false);

        _chooseBaby.ChoiceOfBaby();
    }
}
