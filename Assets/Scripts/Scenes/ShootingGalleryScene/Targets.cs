using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGallery
{
    public class Targets : MonoBehaviour
    {
        public event Action OnStart;
        public event Action SpawnNextTarget;
        public event Action OnEnd;
        private Quaternion _originalRotation;

        [SerializeField] private List<GameObject> sprites;
        [SerializeField] private Collider2D collider;
        [SerializeField] private float rotationDuration;

        private Tweener _currentTween;
        
        private int maxTargetMovePointsInRow = 6;
        private bool isMoving = true;
        public GameObject spawnPosition { get; set; }
        public int randomsprite { get;  set; } = 0;
        public float speed { get;  set; } = 1;
        public int id { get;  set; } = -1;
        public int movementIndicator { get;  set; } = 1;
        public int movePosInRow { get;  set; } = 0;
        public bool hardMode { get;  set; } = false;
        public bool isfree { get;  set; } = false;
        

        public void Construct()
        {
            randomsprite = UnityEngine.Random.Range(0, 12);
            sprites[randomsprite].SetActive(true);
            _originalRotation = transform.rotation;
        }

        // Select eaze or hard move
        private void SelectMove()
        {
            if(isMoving)
            {
                if (hardMode)
                {
                    MoveHard();
                }
                else
                {
                    MoveEaze();
                }
            }
        }

        // Spawn target after move end 
        public void SpawnAgain()
        {
            isMoving = true;
            isfree = false;
            SelectMove();
        }

        // move target in a straight line
        private void MoveEaze() 
        {
            if (movePosInRow < maxTargetMovePointsInRow)
            {             
                movePosInRow++;        
                Sequence rightEaze = DOTween.Sequence();
                rightEaze.Append(this.gameObject.transform.DOMoveX(this.gameObject.transform.position.x - 2f * movementIndicator, speed).SetEase(Ease.Flash));
                rightEaze.AppendCallback(EndMove);
                rightEaze.Play();              
            }
            else
            {
                OnEnd?.Invoke();
            }
        }

        // move target in a curved line
        private void MoveHard()
        {
            if (movePosInRow < maxTargetMovePointsInRow)
            {
                movePosInRow++;         
                if (movePosInRow%2 == 0)
                {
                    Sequence rightHard = DOTween.Sequence();
                    rightHard.Append(this.gameObject.transform.DOMove(new Vector3(this.gameObject.transform.position.x - 2f * movementIndicator, this.gameObject.transform.position.y - 0.25f, this.gameObject.transform.position.z), speed).SetEase(Ease.Flash));
                    rightHard.AppendCallback(EndMove);
                    rightHard.Play();
                }
                else
                {
                    Sequence rightHard = DOTween.Sequence();
                    rightHard.Append(this.gameObject.transform.DOMove(new Vector3(this.gameObject.transform.position.x - 2f * movementIndicator, this.gameObject.transform.position.y + 0.25f, this.gameObject.transform.position.z), speed).SetEase(Ease.Flash));
                    rightHard.AppendCallback(EndMove);
                    rightHard.Play();
                }         
            }
            else
            {
                OnEnd?.Invoke();
            }

        }

        //after every move segment end 
        private void EndMove()
        {          
            if (movePosInRow == 1)
            {
                OnStart?.Invoke();
            }
            else if (movePosInRow == 2)
            {
                SpawnNextTarget?.Invoke();
            }
            SelectMove();
        }

        //when ball  Success Shoot at  target
        public void DestoyTarget()
        {
            collider.enabled = false;
            sprites[randomsprite].SetActive(false);  
        }
        public void ChangeSpriteRotation(float newRotationX)
        {
            foreach (GameObject spriteObject in sprites)
            {
                if (spriteObject.activeSelf)
                {
                    Vector3 newRotation = spriteObject.transform.localRotation.eulerAngles;
                    newRotation.x = newRotationX;
                    spriteObject.transform.DOLocalRotate(newRotation, rotationDuration).OnComplete(() => spriteObject.transform.rotation = _originalRotation);
                }
                StartCoroutine(DestoyTargetWithDelay());
            }
        }
         private IEnumerator DestoyTargetWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            DestoyTarget();
            transform.rotation = _originalRotation;
        }
        //after all move segment end 
        public void DestoyTargetInEnd()
        {
            isfree = true;
            collider.enabled = true;
            movePosInRow = 0;
            gameObject.transform.position = spawnPosition.transform.position;
            sprites[randomsprite].SetActive(false);
            randomsprite = UnityEngine.Random.Range(0, 12);
            sprites[randomsprite].SetActive(true);
        }
        public void StopMoving()
        {
            isMoving = false;
            if (_currentTween != null && _currentTween.IsActive())
            {
                _currentTween.Kill();
            }
        }

        //Change move Direction
        public void ChangeDirection(Vector3 startpoint)
        {     
            movementIndicator *= -1;
            movePosInRow = maxTargetMovePointsInRow  - movePosInRow;
            spawnPosition.transform.position = startpoint;
        }

        // Start is called before the first frame update
        void Start()
        {

            OnEnd += DestoyTargetInEnd;
            SelectMove();
        }

    }
}
