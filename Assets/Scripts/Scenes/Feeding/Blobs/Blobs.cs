using System;
using Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace Feeding
{
    public class Blobs : MonoBehaviour
    {
        public event Action isClean;  // ����, ��� ���������, ���� ���� ������

        [SerializeField] private FeedingSceneController _config;
        [SerializeField] private List<BlobClean> _blobsstates; // ������ ����� ����� (���䳿 ��������)
        public InputSystem _inputSystem;
        public int blobsCount = 0;
        public int maxBlobsCount = 6;  // ����������� ������� �����
        private Vector2 _clampedYPosition = default;  // ��������� ������� �� �� Y

        public float requestCleaningPosition = 400; // ³������, �� ��� ������� �������� ����� (����� �� ����������)

        // ����������� ��� �����
        public void Construct()
        {
            // TODO: �������� 䳿 ��� ������������ �����
        }

        // ��������� ����� �� ��� ���������
        public bool EatingFood()
        {
            if (blobsCount < maxBlobsCount)
            {
                return true;  // ��������� ��������� �����
            }
            return false;  // ����������� ������� ����� ��� ���������
        }

        // ��������� ����� ���� ���������
        public void EatingFoodSpawnBlob()
        {
            if (blobsCount < maxBlobsCount)
            {
                Debug.Log(_blobsstates[blobsCount]);
                _blobsstates[blobsCount].gameObject.SetActive(true);
                blobsCount++;  // ��������� ��������� �����
            }
        }

        // ���������� �����
        public bool NapkinCleaning(BlobClean blob)
        {
            // ���������� ������ ��� ����������
            if (blobsCount > 0)
            {
                Vector3 newPosition = _inputSystem.CalculateTouchPosition();
                if (_clampedYPosition != default)
                {
                    newPosition.y = Mathf.Clamp(newPosition.y, _clampedYPosition.x, _clampedYPosition.y);
                }

                blob.cleaningPosition += newPosition.x + newPosition.y;
                // ���� ��������� ����� � ��������� �� ������� ����������
                blob.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - blob.cleaningPosition / requestCleaningPosition + 0.2f);
            }

            // ���������� 1 �����
            if (blobsCount > 0 && blob.cleaningPosition > requestCleaningPosition)
            {
                blob.gameObject.SetActive(false);
                blob.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                blobsCount--;  // ��������� ��������� �����
                blob.cleaningPosition = 0;  // �������� ��������� ����������

                isClean?.Invoke();  // ������ ��䳿 "�����"
                return true;  // ���������� ������ ��������
            }
            return false;  // ����������� ��������
        }
    }
}
