using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTimeSaver : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _prefAnimationName;

    void Awake()
        => _animator = GetComponent<Animator>();
    private void LoadData()
    {
        float animationTime = PlayerPrefs.GetFloat(_prefAnimationName, 0);
        _animator.Play(0, 0, animationTime);
    }
    private void SaveAnimationState()
    {
        float currentTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        PlayerPrefs.SetFloat(_prefAnimationName, currentTime);
        PlayerPrefs.Save();
    }
    private void OnEnable() => LoadData();
    private void OnDisable() => SaveAnimationState();
}