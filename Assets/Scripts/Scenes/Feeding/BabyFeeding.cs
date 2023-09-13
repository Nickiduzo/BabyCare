using Sound;
using System.Collections;
using UnityEngine;

namespace Feeding
{
    public class BabyFeeding : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        [SerializeField] internal Animator babyAnimator;
        [SerializeField] internal Blobs blobs;

        [SerializeField] private SoundSystem _soundSystem;

        public bool eatAnimationPlay = false; // Прапорець, що вказує, чи триває анімація їжі.

        public void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        // Рандомно обирається анімація і звук після того як дитина поїла і забруднилась
        public void EatingFoodSpawnBlob()
        {

            int afterEatID = Random.Range(1, 4);

                switch (afterEatID)
                {
                    case 1:
                        StartCoroutine(PlayAnimation("BabyAfterEat", 1.1f));
                        StartCoroutine(PlaySoundWithDelay("burp", 2f)); // Запускаємо звук "burp" з затримкою.
                        break;
                    case 2:
                        StartCoroutine(PlayAnimation("BabyAfterEat2", 2.7f));
                        StartCoroutine(PlaySoundWithDelay("laugh", 2.7f)); // Запускаємо звук "laugh" з затримкою.
                        break;
                    default:
                        babyAnimator.Play("BabyAfterEat3");
                        StartCoroutine(PlaySoundWithDelay("swallowing", 0.01f));
                        break;

                }

            blobs.EatingFoodSpawnBlob();
        }

        // Корутина яка відповідає за відтворення анімації
        private IEnumerator PlayAnimation(string name, float delay)
        {
            yield return new WaitForSeconds(delay);
            babyAnimator.Play(name);
        }

        // Корутина яка відповідає за відтворення звуку
        private IEnumerator PlaySoundWithDelay(string soundName, float delay)
        {
            yield return new WaitForSeconds(delay); // Затримка перед відтворенням звуку.
            _soundSystem.PlaySound(soundName);
        }

        // Метод який зупиняє звук і анімацію коли дитина брудна, викликається у EatZone
        public void StopAfterEatAnimations()
        {
            StopAllCoroutines();
        }

        public void EatAnimationEnd()
        {
            babyAnimator.Play("BabyAfterEat3");
            eatAnimationPlay = false; // Завершення анімації їжі.
        }
    }
}
