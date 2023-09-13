using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class AppearWithDuration : MonoBehaviour
    {
        [SerializeField] protected float _appearDuration;
        public abstract Tween Appear();
    }
}