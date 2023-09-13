using DG.Tweening;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    [SerializeField] float _arrowMoveDuration;

    private Transform _rotateHintTrans;

    private void Start()
    {
        _rotateHintTrans = _arrow.GetComponent<Transform>();
    }

    public Tween MoveAheadArrow()
      => _arrow.transform.DOMoveY(_rotateHintTrans.position.y - 0.5f, _arrowMoveDuration);

    public void ShowArrow()
    => _arrow.SetActive(true);


    public void HideArrow()
    => _arrow.SetActive(false);
}