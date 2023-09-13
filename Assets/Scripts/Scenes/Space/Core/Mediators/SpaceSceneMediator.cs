using Sound;
using System.Collections;
using System.Collections.Generic;
using UI;
//using UnityEditor.Timeline.Actions;
using UnityEngine;

public class SpaceSceneMediator : MonoBehaviour
{
    [SerializeField] SpaceLevelConfig _config;
    [SerializeField] SoundSystem _soundSystem;
    [SerializeField] ActorUI _actorUI;
    [SerializeField] TimerView _timer;
    [SerializeField] MoveController _moveController;

    #region Singleton
    public static SpaceSceneMediator Instance { get; private set; }

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
    }
    #endregion Singleton

    private void Start()
    {
        _actorUI.InitWinInvoker(_timer);
        _timer.OnWin += () => _moveController.IsValidate = false;

        StarView.OnCollectStar.AddListener((ICollectable obj) => _actorUI.Hud.AddScore(obj));
        StarView.OnCollectStar.AddListener((_) => _soundSystem.PlaySound("StarCollect"));
        StarView.OnCertainAmount.AddListener((int time) => _actorUI.Hud.IncreaseTime(time));
    }

    private void OnDestroy()
    {
        StarView.OnCertainAmount.RemoveAllListeners();
        StarView.OnCollectStar.RemoveAllListeners();
    }
}
