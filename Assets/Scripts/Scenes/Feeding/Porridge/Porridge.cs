using DG.Tweening;
using Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Feeding
{
    public class Porridge : MonoBehaviour
    {

        [SerializeField] private FeedingSceneController _config;
        [SerializeField] private List<GameObject> _porridgestates; //the fullness of the plate(the amount of porridge that is left)

        public int _SpoonEatCount;
        public int _MaxSpoonEatCount = 3;

        //when construct porridge gameObject
        public void Construct() { }

        //when porridge are eating 
        public bool EatingPorridge()
        {
            if (_SpoonEatCount < _MaxSpoonEatCount)
            {
                _porridgestates[_SpoonEatCount].SetActive(false);
                _SpoonEatCount++;
                _porridgestates[_SpoonEatCount].SetActive(true);
                return true;
            }
            return false;

        }


    }
}
