using DG.Tweening;
using Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Feeding
{
    public class Apple : MonoBehaviour
    {
        public event Action Dragging;

        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private List<GameObject> _applestates; //apple eating states
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject shadow;
       
        private int AppleEatCount = 0;
        private int MaxAppleEatCount = 3;
        public bool drop; // flag what Drag End

        public void Awake()
        {
            Construct(_inputSystem);

        }

        //when construct apple gameObject
        public void Construct(InputSystem inputSystem)
        {
            _dragAndDrop.Construct(inputSystem);
            _destinationOnDragEnd.Construct(transform.position);
            _dragAndDrop.OnDragStart += DragStart;
            _destinationOnDragEnd.OnMoveComplete += OnShadow;
            _dragAndDrop.OnDragEnded += DragEnd;
        }

        //when apple are eating 
        public bool EatingApple()
        {
            if (AppleEatCount < MaxAppleEatCount)
            {
                _applestates[AppleEatCount].SetActive(false);
                AppleEatCount++;
                _applestates[AppleEatCount].SetActive(true);
                if (AppleEatCount == MaxAppleEatCount)
                {
                    return false;
                }
                return true;
            }
            return false;

        }

        public void DragEnd()
        {
            drop = true;
        }
        private void OnShadow()
        {
            shadow.SetActive(true);
            drop = false;
        }

        private void DragStart()
        {
            shadow.SetActive(false); // off shadow
            drop = false;
        }


    }
}
