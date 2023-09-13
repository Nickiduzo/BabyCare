using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerSystem : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Vector2 _dynamicPos;
    [SerializeField] float _rotateValue, _moveValue, _duration, _interval;
    [SerializeField] bool _isValid = true;

    public bool IsValid { get { return _isValid; } set { _isValid = value; } }

    #region Singleton
    public static HummerSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        Hide();
    }
    #endregion Singleton

    public void Show(Vector2 target, TweenCallback callback,Vector2 dynamicPos = default, float scale = 1f)
    {
        if (!_isValid) return;

        var addPos = (dynamicPos != default) ? target + dynamicPos
                                                : target + _dynamicPos;
        transform.position = addPos;
        transform.localScale = new Vector3(scale, scale, scale);
        _spriteRenderer.enabled = true;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + new Vector3(0, _moveValue), _duration));
        sequence.AppendInterval(_interval);
        sequence.Join(transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, _rotateValue), _duration));
        sequence.AppendCallback(Hide);
        sequence.SetAutoKill();
        sequence.OnComplete(callback);
    }

    public void Hide()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        _spriteRenderer.enabled = false;
    }
}
