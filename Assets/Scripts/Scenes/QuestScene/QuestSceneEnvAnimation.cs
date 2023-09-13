using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBaby
{
    public class QuestSceneEnvAnimation : MonoBehaviour
    {
        [SerializeField] private FxSystem fxSystem;

        [SerializeField] private GameObject horse;
        [SerializeField] private GameObject teddybear;
        [SerializeField] private GameObject window;
        [SerializeField] private GameObject paint;

        // Start is called before the first frame update
        void Start()
        {
            ITouchSystem inputSystem = FindObjectOfType<BodyController>();
            inputSystem.OnTapped += HandleTap;
        }

        //on tap 
        private void HandleTap(Vector3 touchPosition)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null) SelectAnimationMethod(hit.collider.gameObject);
        }

        //  Select Animation
        private void SelectAnimationMethod(GameObject gameObject)
        {
           
            if (gameObject.name == horse.name) { PlayHorseAnimation(); }
            else if (gameObject.name == teddybear.name) { PlayTeddybearAnimation(); }
            else if (gameObject.name == window.name) { PlayWindowAnimation(); }
            else if (gameObject.name == paint.name) { PlayPaintParticle(); }


        }

        private void PlayHorseAnimation()
        {
            horse.GetComponent<Animation>().Play("horse");
        }
        private void PlayTeddybearAnimation()
        {
            teddybear.GetComponent<Animation>().Play("bear");
        }
        private void PlayWindowAnimation()
        {
            window.GetComponent<Animator>().SetBool("open", !window.GetComponent<Animator>().GetBool("open"));
        }
        private void PlayPaintParticle()
        {
            fxSystem.PlayEffect("heart", paint.transform.position);
        }

    }
}
