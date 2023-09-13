
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Feeding
{
    public class BlobClean : MonoBehaviour
    {
    
        [SerializeField] private NapkinObserver _observerNapkin;
        [SerializeField] public Blobs blobs;
        public float cleaningPosition; // distance for cleaning
        private void Awake()
        {   
            _observerNapkin.OnTriggerStay += Process;
        }

        private void OnDestroy()
        {
            _observerNapkin.OnTriggerStay -= Process;

        }


        //when wiping a child from blobs
        private void Process(Napkin napkin)
        {
            blobs.NapkinCleaning(this);        
        }
    }
}
