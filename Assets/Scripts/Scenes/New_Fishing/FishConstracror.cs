using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    public class FishConstracror : MonoBehaviour
    {
        [SerializeField] private List<Fish> fishList;
        public Fish fish { get; set; }

        public void Awake()
        {
          //  Construct();
        }

        //when construct FishConstracror gameObject
        public void Construct()
        {
            var createdObj = Object.Instantiate(fishList[Random.Range(0, 5)], gameObject.transform);
            createdObj.gameObject.SetActive(true);

            fish = createdObj;

        }
    }
}
