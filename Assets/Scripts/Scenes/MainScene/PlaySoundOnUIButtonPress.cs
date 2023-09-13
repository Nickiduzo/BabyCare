using Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySoundOnUIButtonPress : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SoundSystem _soundSystem;

    private const string UIButtonSoung = "buttonUIClick";

    /// <summary>
    /// Вводимо дані події [eventData] - якщо у джерела даних події [eventData] є кнопка,
    /// то відтворює звук натискання кнопки
    /// </summary>    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        if (eventData.pointerPress.GetComponent<Button>() != null)
        {
            _soundSystem.PlaySound(UIButtonSoung);
        }
    }
}
