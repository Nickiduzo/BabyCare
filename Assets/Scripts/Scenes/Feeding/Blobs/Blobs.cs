using System;
using Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace Feeding
{
    public class Blobs : MonoBehaviour
    {
        public event Action isClean;  // Подія, яка спрацьовує, коли блоб чистий

        [SerializeField] private FeedingSceneController _config;
        [SerializeField] private List<BlobClean> _blobsstates; // Список станів блобів (стадії брудності)
        public InputSystem _inputSystem;
        public int blobsCount = 0;
        public int maxBlobsCount = 6;  // Максимальна кількість блобів
        private Vector2 _clampedYPosition = default;  // Обмеження позиції по осі Y

        public float requestCleaningPosition = 400; // Відстань, на яку потрібно прибрати блоба (запит на прибирання)

        // Конструктор для блобів
        public void Construct()
        {
            // TODO: Додаткові дії при конструюванні блобів
        }

        // Додавання блоба під час годування
        public bool EatingFood()
        {
            if (blobsCount < maxBlobsCount)
            {
                return true;  // Можливість додавання блоба
            }
            return false;  // Максимальна кількість блобів вже досягнута
        }

        // Додавання блоба після годування
        public void EatingFoodSpawnBlob()
        {
            if (blobsCount < maxBlobsCount)
            {
                Debug.Log(_blobsstates[blobsCount]);
                _blobsstates[blobsCount].gameObject.SetActive(true);
                blobsCount++;  // Збільшення лічильника блобів
            }
        }

        // Прибирання блоба
        public bool NapkinCleaning(BlobClean blob)
        {
            // Розрахунок відстані для прибирання
            if (blobsCount > 0)
            {
                Vector3 newPosition = _inputSystem.CalculateTouchPosition();
                if (_clampedYPosition != default)
                {
                    newPosition.y = Mathf.Clamp(newPosition.y, _clampedYPosition.x, _clampedYPosition.y);
                }

                blob.cleaningPosition += newPosition.x + newPosition.y;
                // Зміна прозорості блоба в залежності від ступеня прибирання
                blob.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - blob.cleaningPosition / requestCleaningPosition + 0.2f);
            }

            // Прибирання 1 блоба
            if (blobsCount > 0 && blob.cleaningPosition > requestCleaningPosition)
            {
                blob.gameObject.SetActive(false);
                blob.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                blobsCount--;  // Зменшення лічильника блобів
                blob.cleaningPosition = 0;  // Скидання лічильника прибирання

                isClean?.Invoke();  // Виклик події "чисто"
                return true;  // Прибирання успішно виконане
            }
            return false;  // Недостатньо прибрано
        }
    }
}
