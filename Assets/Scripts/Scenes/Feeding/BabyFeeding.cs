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

        public bool eatAnimationPlay = false; // ���������, �� �����, �� ����� ������� ��.

        public void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        // �������� ��������� ������� � ���� ���� ���� �� ������ ���� � ������������
        public void EatingFoodSpawnBlob()
        {

            int afterEatID = Random.Range(1, 4);

                switch (afterEatID)
                {
                    case 1:
                        StartCoroutine(PlayAnimation("BabyAfterEat", 1.1f));
                        StartCoroutine(PlaySoundWithDelay("burp", 2f)); // ��������� ���� "burp" � ���������.
                        break;
                    case 2:
                        StartCoroutine(PlayAnimation("BabyAfterEat2", 2.7f));
                        StartCoroutine(PlaySoundWithDelay("laugh", 2.7f)); // ��������� ���� "laugh" � ���������.
                        break;
                    default:
                        babyAnimator.Play("BabyAfterEat3");
                        StartCoroutine(PlaySoundWithDelay("swallowing", 0.01f));
                        break;

                }

            blobs.EatingFoodSpawnBlob();
        }

        // �������� ��� ������� �� ���������� �������
        private IEnumerator PlayAnimation(string name, float delay)
        {
            yield return new WaitForSeconds(delay);
            babyAnimator.Play(name);
        }

        // �������� ��� ������� �� ���������� �����
        private IEnumerator PlaySoundWithDelay(string soundName, float delay)
        {
            yield return new WaitForSeconds(delay); // �������� ����� ����������� �����.
            _soundSystem.PlaySound(soundName);
        }

        // ����� ���� ������� ���� � ������� ���� ������ ������, ����������� � EatZone
        public void StopAfterEatAnimations()
        {
            StopAllCoroutines();
        }

        public void EatAnimationEnd()
        {
            babyAnimator.Play("BabyAfterEat3");
            eatAnimationPlay = false; // ���������� ������� ��.
        }
    }
}
