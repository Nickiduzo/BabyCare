using Inputs;
using System;
using UnityEngine;
using System.Collections;

namespace ShootingGallery
{
    public class BallShoot : MonoBehaviour
    {
        public event Action SuccessShoot;
        public event Action MissShoot;
        public Rigidbody2D rb;

        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public Animator _animator;
        [SerializeField] private TargetsObserver _observerTarget;
        [SerializeField] private LayerMask _column;
        [SerializeField] private BallMusic _ballMusic;

        public InputSystem InputSystem => _inputSystem;
        public Animator Animator => _animator;
        public TargetsObserver TargetObserver => _observerTarget;
        public LayerMask ColumnLayerMask => _column;
        public FxSystem fxSystem { get; set; }
        public Vector3 spawmPos { get; set; }
        public bool isWinPanelActive { get; set; }
        public bool isSpawnBallAnimationPlaying { get; set; }
        public bool defoultmiss { get; set; }
        public bool isMissed { get; set; }
        public bool leftColumn { get; set; }
        public bool rightColumn { get; set; }
        public bool isHit { get; set; }
        public bool shootFlag { get; set; }
        public bool isFlyingTowardsScreen { get; set; }
        public bool isFalling { get; set; }

        public Renderer rendererComponent;
        private Targets activeTarget;

        public Vector3 leftColumnSpawn;
        public Vector3 rightColumnSpawn;
        public Vector3 missPosition;

        private float moveSpeed;
        private float hitThreshold = 3f;
        private float minScale = 0;
        public int newSortingOrder = 2;
        public int defoultSortingOrder = 10;
        private float hitBackDistance = 0.5f;


        private bool isBallMovingToSpawn = false;
        private bool isScaling = false;
        private bool isFallingBack = false;

        public void Awake()
        {
            Construct(_inputSystem);    
            rb = GetComponent<Rigidbody2D>();
            rendererComponent = GetComponentInChildren<Renderer>();
            _ballMusic = GetComponent<BallMusic>();
            _dragAndDrop.OnDragStart += DragBallStart;
        } 
            
        //when construct ball gameObject
        public void Construct(InputSystem inputSystem)
        {
            _dragAndDrop.Construct(inputSystem);
            _dragAndDrop.OnDragEnded += StartDropBall;
            _observerTarget.OnTriggerStay += DropBallOnTarget;

        }
        //when ball hit target
        private void DropBallOnTarget(Targets target)
        {
            if (shootFlag)
            {
                Vector2 direction = target.transform.position - transform.position;
                if (Physics2D.Linecast(transform.position, target.transform.position, LayerMask.GetMask("Column")))
                {
                    return;
                }
                _ballMusic.PlaySound("Hit");
                SuccessShoot?.Invoke();
                fxSystem.PlayEffect("Success", transform.position);
                shootFlag = false;
                activeTarget = target;
                activeTarget?.ChangeSpriteRotation(90.0f);
            }
        }
        // private void OnDestroy()
        // {
        //     _dragAndDrop.OnDragStart -= DragBallStart;
        // }

        //when take ball
        private void DragBallStart()
        {

            _ballMusic.PlaySound("Taking");
        }
        //check Miss Shoot
        private void GoodGame()
        {
            if(isHit)
            {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, missPosition, _column);               
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("LeftColumn") && Vector2.Distance(hit.point, missPosition) <= hitThreshold)
                    {
                        isFlyingTowardsScreen = true;
                        leftColumn = true;
                        rightColumn = false;
                        transform.position = missPosition;
                        defoultmiss = false;
                    }
                    else if (hit.collider.gameObject.CompareTag("RightColumn") && Vector2.Distance(hit.point, missPosition) <= hitThreshold)
                    {
                        isFlyingTowardsScreen = true;
                        rightColumn = true;
                        leftColumn = false;
                        transform.position = missPosition;
                        defoultmiss = false;
                    }
                }
                _animator.enabled = false;
                _ballMusic.PlaySound("Miss");
                MissShoot?.Invoke();
                isHit = false;  
            } 
        }
        //when start drop ball
        private void StartDropBall()
        {
            if(isSpawnBallAnimationPlaying)
            {
                _animator.enabled = true;
                _animator.Rebind();
                _animator.Play("drop", 1, 0f);
                isSpawnBallAnimationPlaying = false;
            }
            else
            {
                _animator.SetBool("drop", true);
            }
            _dragAndDrop.IsDraggable = false;
            _animator.SetBool("drop", true);
            missPosition = transform.position;
            isMissed = true;
            isBallMovingToSpawn = false;
        }
        //when does the ball drop animation end
        private void CheckTarget()
        {
            shootFlag = true;
            isHit = true;
            isFlyingTowardsScreen = true;
        }

        // back ball to a table after drop
        public void SpawnBallAgain()
        {
            shootFlag = false;
            rendererComponent.sortingOrder = defoultSortingOrder;
            transform.position = spawmPos;
            _animator.SetBool("drop", false);
            StartCoroutine(IsDraggableWithDelay());
            isFalling = false;
        }
        //animation where miss
        public void Rebound()
        {
            shootFlag = false;
            transform.position = missPosition;
            defoultmiss = false;

        }
        //animation default miss
        private void Update() 
        {
            if(defoultmiss && !leftColumn && !rightColumn && !isScaling && !isFalling)
            {
                StartCoroutine(ScaleBallWithDelay());
            }
        }
        //check miss on column
        public void FixedUpdate()
        {
            if (isFlyingTowardsScreen)
            {   
                Vector3 targetPosition;

                if (leftColumn)
                {
                    targetPosition = leftColumnSpawn;
                }
                else if (rightColumn)
                {
                    targetPosition = rightColumnSpawn;
                }
                else
                {
                    return;
                }

                if (isMissed)
                {
                    rb.velocity = Vector3.zero; 
                    transform.position = missPosition;
                    isMissed = false;

                    Vector3 targetSpawnPosition = leftColumn ? leftColumnSpawn : rightColumnSpawn;
                    StartCoroutine(MoveToSpawnPosition(targetSpawnPosition));
                    isBallMovingToSpawn = true;
                    if (isBallMovingToSpawn)
                    {
                        _dragAndDrop.IsDraggable = false;
                    }
                }                       
            }           
        }
        //delay before spawn
        private IEnumerator IsDraggableWithDelay()
        {
            yield return new WaitForSeconds(0.2f);
            if(!isWinPanelActive)
            {
                _dragAndDrop.IsDraggable = true;
            }    
        }
        //delay before miss
        private IEnumerator ColumnIsDraggable()
        {
            yield return new WaitForSeconds(0.5f);
            if(!isWinPanelActive)
            {
                _dragAndDrop.IsDraggable = true; 
            }
        }
        //animation on miss
        private IEnumerator ScaleBallWithDelay()
        {
            Rebound();
            isScaling = true;
            float moveSpeed = 2f;
            float newScale = transform.localScale.x;
            StartCoroutine(ColumnIsDraggable());

            while (newScale > minScale)
            {
                transform.position = missPosition;
                newScale -= Time.deltaTime * moveSpeed;
                transform.localScale = Vector3.one * newScale;
                yield return null; 
            }
            if(newScale < minScale)
            {
                isFlyingTowardsScreen = false;
                defoultmiss = false;
                transform.localScale = Vector3.one;
                transform.position = spawmPos;
            }
            isScaling = false;
            _animator.Rebind();
            _animator.enabled = true;
        }
        //animation on miss on column
        private IEnumerator MoveToSpawnPosition(Vector3 targetSpawnPosition)
        {
            Rebound();
            StartCoroutine(ColumnIsDraggable());
            float moveSpeed = 15f;
            while (Vector3.Distance(transform.position, targetSpawnPosition) > 0.1f)
            {
                Vector3 direction = (targetSpawnPosition - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                yield return null;
            }
            isFlyingTowardsScreen = false;
            leftColumn = false;
            rightColumn = false;
            defoultmiss = false;
            isSpawnBallAnimationPlaying = true;
            _animator.Rebind();
        }
    }  
}
