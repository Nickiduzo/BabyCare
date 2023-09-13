using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _parentalControl;

    private void OnEnable()
    {
        StartCoroutine(ShowParentControll());
    }

    private IEnumerator ShowParentControll()
    {
        //_animator.SetBool("SettingsPanel", true);
        _parentalControl.SetActive(true);

        yield return new WaitUntil(() => AnimationChecker.Instance.IsAnimationOver(_animator, "Popup"));

        yield return new WaitForSeconds(0f);

       

        yield break;
    }


    public void Hide()
    {
        StartCoroutine(HideCoroutine());

    }
    private IEnumerator HideCoroutine()
    {
        //_animator.SetBool("SettingsPanel", false);
        _animator.SetTrigger("Hide");

        yield return new WaitUntil(() => AnimationChecker.Instance.IsAnimationOver(_animator, "Hide"));

        gameObject.SetActive(false);

        yield break;
    }
}
