using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulComponents;

namespace ShootingGallery
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private BallShoot _ballShoot;
        private bool hitOccurred = false;
        private Vector3 leftHandPosition;
        private Vector3 rightHandPosition;
        private Vector3 endLocation; 
        private Vector3 startLocation; 
        
        float pointerScale = 1.5f;

        //hint where to shoot
        void Start()
        {
            leftHandPosition = new Vector3(-6.280001f, -3.54f, 0f);
            rightHandPosition = new Vector3(6.28f, -3.54f, 0f);

            startLocation = new Vector3(0.43f, -2.59f, 0f);
            Vector3 endLocation = new Vector3(2f, 0f, 0f);
            HintSystem.Instance.ShowPointerHint(startLocation, endLocation, pointerScale);

            _ballShoot.MissShoot += Ball_MissShoot;
        }
        private void Ball_MissShoot()
        {
            if (_ballShoot.leftColumn)
            {
                HintSystem.Instance.ShowPointerHint(leftHandPosition, endLocation, pointerScale);
            }
            else if (_ballShoot.rightColumn)
            {
                HintSystem.Instance.ShowPointerHint(rightHandPosition, endLocation, pointerScale);
            }
            else
            {
                HintSystem.Instance.ShowPointerHint(startLocation, endLocation, pointerScale);
            }
        }
        //hint destroy when hit
        public void OnHit()
        {
            if (!hitOccurred)
            {
                hitOccurred = true;
                HintSystem.Instance.HidePointerHint();
                _ballShoot.MissShoot -= Ball_MissShoot; 
            }
        }
    }
}
// if(_ballShoot.leftColumn)
//             {
//             Vector3 startLocation = new Vector3(-6.280001f, -3.54f, 0f);
//             Vector3 endLocation = new Vector3(2f, 0f, 0f);
//             float pointerScale = 1.5f;
//             HintSystem.Instance.ShowPointerHint(startLocation, endLocation, pointerScale);
//             }
//             else if (_ballShoot.rightColumn)
//             {
//                 Vector3 startLocation = new Vector3(6.28f, -3.54f, 0f);
//                 Vector3 endLocation = new Vector3(2f, 0f, 0f);
//                 float pointerScale = 1.5f;
//                 HintSystem.Instance.ShowPointerHint(startLocation, endLocation, pointerScale);
//             }
