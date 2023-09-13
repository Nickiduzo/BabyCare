using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using System;
using UsefulComponents;


namespace ShootingGallery
{
    public class ShootingGallerySceneMediator : MonoBehaviour
    {
        public event Action StopMoving;
        [SerializeField] private FxSystem _fxSystem;
        [SerializeField] private TargetSpawn _targetSpawn;
        [SerializeField] private GameObject _pointsTop;
        [SerializeField] private GameObject _pointsDown;
        [SerializeField] private BallShootSpawn _SpawnBall;
        [SerializeField] private BallMusic _ballMusic;
        [SerializeField] private LayerMask _shooting;
        public GameObject PointsTop => _pointsTop;
        public GameObject PointsDown => _pointsDown;
        public LayerMask ShootingLayerMask => _shooting;

        [SerializeField] private WinPannel _WinPannel;
        [SerializeField] private Timer _timer;
        [SerializeField] private ScoreCount _score;
        [SerializeField] private float _bonusTime;
        [SerializeField] private float _difficultHard;
        [SerializeField] private float _moveDirection;
        [SerializeField] private GameObject _clockPrefab;
        private GameObject _clockInstance;
        private Vector3 clockPos;
        private Vector3 missPos;

        private BallShoot _ball;
        private List<Targets> _targetsUp;
        private List<Targets> _targetsDown;
        private int score = 0;
        private bool _targetMoveDir = true;
        private bool _isGameActive = true;
        private bool _isFallingBack = false;
        private Quaternion _originalRotation;

        private void Awake()
        {
            _targetsUp = new List<Targets>();
            _targetsDown = new List<Targets>();
        }

        //Spawn All Targets in top row
        private void SpawnAllTargetsUp()
        {
           for(int i = 0; i < 7; i++) { 
                Targets target = _targetSpawn.SpawnTargets(new Vector3(_pointsTop.transform.position.x - 2f*i, _pointsTop.transform.position.y , _pointsTop.transform.position.z));
                target.movePosInRow = i;
                target.id = 6 -i;
                target.spawnPosition = _pointsTop;
                _targetsUp.Add(target);
                target.Construct();
               
            }

           foreach(Targets t in _targetsUp)
            {
                t.SpawnNextTarget += SpawnTargetsUp;
            }
        }

        //Spawn All Targets in bottom row
        private void SpawnAllTargetsDown()
        {
            for (int i = 0; i < 7; i++)
            {
                Targets target = _targetSpawn.SpawnTargets(new Vector3(_pointsDown.transform.position.x + 2f * i, _pointsDown.transform.position.y, _pointsDown.transform.position.z));
                target.movementIndicator *= -1;
                target.movePosInRow = i;
                target.id = 6 - i;
                target.spawnPosition = _pointsDown;
                _targetsDown.Add(target);
                target.SpawnNextTarget += SpawnTargetsDown;         
                target.Construct();
        
            }
        }

        //Spawn one Target in top row
        private void SpawnTargetsUp()
        {
            if (_isGameActive)
            {
                _targetsUp.Find(x => x.isfree ==true).SpawnAgain();
            }      
        }

        //Spawn one Target in bottom row
        private void SpawnTargetsDown()
        {
            if (_isGameActive)
            {
                _targetsDown.Find(x => x.isfree == true).SpawnAgain();
            }    
        }

        //change targets move to a straight line
        private void SetTargetsDifficultHard()
        {
            foreach(Targets t in _targetsUp)
            {
                t.hardMode = true;
            }
            foreach (Targets t in _targetsDown)
            {
                t.hardMode = true;
            }
        }

        //Change all Targets move Direction
        private void ChangeTargetsMoveDirection()
        {
            _targetMoveDir = !_targetMoveDir; 
            Vector3 top = new Vector3(0 ,0 ,0);
            if (_targetsUp[0].movementIndicator == 1)
            {
                top = new Vector3(-12.7f + _pointsTop.transform.position.x, _pointsTop.transform.position.y, _pointsTop.transform.position.z);
            }
            else if (_targetsUp[0].movementIndicator == -1)
            {
                top = new Vector3(12.7f + _pointsTop.transform.position.x, _pointsTop.transform.position.y, _pointsTop.transform.position.z);
            }

            Vector3 down = new Vector3(0, 0, 0);
            if (_targetsDown[0].movementIndicator == 1)
            {
                down = new Vector3(-12.7f  + _pointsDown.transform.position.x, _pointsDown.transform.position.y, _pointsDown.transform.position.z);
            }
            else if (_targetsDown[0].movementIndicator == -1)
            {
                down = new Vector3(12.7f + _pointsDown.transform.position.x, _pointsDown.transform.position.y, _pointsDown.transform.position.z);
            }

            foreach (Targets t in _targetsUp)
            {      
                t.ChangeDirection(top);                     
            }
            foreach (Targets t in _targetsDown)
            {    
                t.ChangeDirection(down);           
            }
        }
        //bonus speed
        private void SpeedUp()
        {
            foreach (Targets t in _targetsUp)
            {
                t.speed -= t.speed * 0.05f;
            }
            foreach (Targets t in _targetsDown)
            {
                t.speed -= t.speed * 0.05f;
            }
        }

        //Spawn Ball
        private BallShoot SpawnBall()
        {
            BallShoot ball = _SpawnBall.SpawnBall();
            ball.fxSystem = _fxSystem;
            ball.SuccessShoot += Ball_SuccessShoot;
            ball.MissShoot += Ball_MissShoot;
            _ball = ball;
            return ball;
        }

        //when player Success Shoot
        private void Ball_SuccessShoot()
        {
            _score.AddScore(1);
            Hint hintScript = FindObjectOfType<Hint>();
            //animation ball falling
            if (!_ball.isFalling)
            {
                _ball.isFalling = true;
                StartCoroutine(ShootingGallery());
            }
            //destroy hint
            if (hintScript != null)
            {
                hintScript.OnHit();
            }
            score++;
            _ball.isHit = false;
            if (score % _bonusTime == 0)
            {
                _ballMusic.PlaySound("BonusTime");
                _timer.AddTime(10);
                if (_clockPrefab != null)
                {
                    if (_clockInstance != null)
                    {
                        Destroy(_clockInstance);
                    }
                    clockPos = _ball.missPosition;
                    _clockInstance = Instantiate(_clockPrefab, clockPos, Quaternion.identity);
                    ClockAnimation clockAnimation = _clockInstance.GetComponent<ClockAnimation>();
                    clockAnimation.ShowClock();
                }
            }
            if(score == _difficultHard)
            {
                SetTargetsDifficultHard();
                _ballMusic.PlaySound("Rules");
            }
            if (score % _moveDirection == 0)
            {
                SpeedUp();
                ChangeTargetsMoveDirection();
                _ballMusic.PlaySound("Rules");
            }
        }
        //when player miss shoot
        public void Ball_MissShoot()
        {
            _score.ResetScore();
            RaycastHit2D hit = Physics2D.Raycast(missPos, Vector2.down, Mathf.Infinity, LayerMask.GetMask("ShootingGallery"));
            missPos = _ball.missPosition;
            if (IsInsideTargetArea(_ball.transform.position))
            {
                _ball.isFalling = true;
                _ball._dragAndDrop.IsDraggable = false;
                _ball.shootFlag = false;
                StartCoroutine(ShootingGallery());
            }
            _ball.defoultmiss = true;
            score = 0;
        }
        //position check
        private bool IsInsideTargetArea(Vector2 point)
        {
            Collider2D ShootingCollider = GameObject.Find("ShootingGallery").GetComponent<Collider2D>();

            return ShootingCollider.OverlapPoint(point);
        }
        //start fall
        public IEnumerator ShootingGallery()
        {
            yield return new WaitForSeconds(0.1f);
            if (_ball.isFalling)
            {
                StartCoroutine(FallBall());
            }   
        }
        //falling animation
        private IEnumerator FallBall()
        {
            float fallSpeed = 1.3f;
            float deltaY = -4.0f;
            Vector3 startPosition = _ball.transform.position;
            Vector3 endPosition = new Vector3(startPosition.x, deltaY, startPosition.z);
            _ball.rendererComponent.sortingOrder = _ball.newSortingOrder;

            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * fallSpeed;
                _ball.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                _ball._animator.enabled = false;
                yield return null;
            }
            _ball.defoultmiss = false;
            _ball.isFalling = false;
            _ball.isSpawnBallAnimationPlaying = true;
            _ball.Animator.Rebind();
            _ball.SpawnBallAgain();
        }
        
        
        //when lvl end 
        public void LvlEnd()
        {
            foreach (Targets target in _targetsUp)
            {
                target.StopMoving();
            }
            foreach (Targets target in _targetsDown)
            {
                target.StopMoving();
            }
            _isGameActive = false;
            _ball.isWinPanelActive = true;
            HintSystem.Instance.HidePointerHint();
            _WinPannel.Appear();
            _ball._dragAndDrop.IsDraggable = false;
            _ball.rendererComponent.enabled = false;  
        }

        // Start is called before the first frame update
        void Start()
        {
            _timer.OnTimeEnd += LvlEnd;
            _isGameActive = true;
            SpawnAllTargetsDown();
            SpawnAllTargetsUp();
            SpawnBall();
        }

    }
}
