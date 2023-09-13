using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestBaby
{
    public class QuestSceneMediator : MonoBehaviour
    {
        [SerializeField] private BabySpawn _spawnBaby;
        [SerializeField] private GameObject birds;
        [SerializeField] private GameObject flyBabyDownEndPoint;
        [SerializeField] private GameObject hud;
        [SerializeField] private List<GameObject> interactiveElements;
        private Baby _baby;

        //spawn baby 
        private Baby SpawnBaby()
        {
            Baby baby = _spawnBaby.SpawnBaby();
            _baby = baby;
            return baby;

        }

        private void FlyBabyDown()
        {
            birds.GetComponent<Animator>().SetBool("move", true);
            Sequence flydown = DOTween.Sequence();
            // show.AppendCallback(startShowAnimation);
            flydown.Append(birds.transform.DOMoveY(flyBabyDownEndPoint.transform.position.y, 5));
            flydown.Join(_baby.transform.DOLocalMoveY(flyBabyDownEndPoint.transform.position.y + 0.2f, 5));
            flydown.AppendCallback(AfterFlyBabyDown); 
            flydown.Play();
        }

        private void AfterFlyBabyDown()
        {
            birds.GetComponent<Animator>().Play("BirdsAfterMoveBabyDownQuestScene");
            hud.SetActive(true);
            OnSceneInteractiveElements();

            EnableBabyColliders();
        }

        private void OffSceneInteractiveElements()
        {
            foreach(GameObject g in interactiveElements)
            {
                g.GetComponent<Collider2D>().enabled = false;
            }
        }
        private void OnSceneInteractiveElements()
        {
            foreach (GameObject g in interactiveElements)
            {
                g.GetComponent<Collider2D>().enabled = true;
            }
        }
        private void DisableBabyColliders()
        {
            Collider2D[] colliders = _baby.GetComponentsInChildren<Collider2D>(true);
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
        }
        private void EnableBabyColliders()
        {
            Collider2D[] colliders = _baby.GetComponentsInChildren<Collider2D>(true);
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SpawnBaby();
            DisableBabyColliders();
            FlyBabyDown();
            hud.SetActive(false);
            OffSceneInteractiveElements();
        }
    }
}
