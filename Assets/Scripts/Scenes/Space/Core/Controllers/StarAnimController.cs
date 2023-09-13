using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarAnimController : MonoBehaviour
{
    [SerializeField] List<StarView> _starViews;
    
    private readonly float _timeForNextStar = 0.1f;

    private void Start()
    {
        StartCoroutine(ShapeAnim());
    }

    IEnumerator ShapeAnim()
    {
        foreach (var star in _starViews)
        {
            if(star.Animator != null)
                star.Animator.SetTrigger("Star");
            yield return new WaitForSeconds(_timeForNextStar);
        }  
    }
}
