using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBaby
{
    public class BirdsQuest : MonoBehaviour
    {
        [SerializeField] private GameObject diaper;
        private void DestroyAfterAnimationEnd()
        {
            Destroy(this.gameObject);
        }
        private void EnebleDiaper()
        {
            diaper.SetActive(true);
        }
        private void Start()
        {
            diaper.SetActive(false);
        }
    }
}
