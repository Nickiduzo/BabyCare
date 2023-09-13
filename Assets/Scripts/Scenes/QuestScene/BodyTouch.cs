using System;
using Sound;
using UnityEngine;

public class BodyTouch : MonoBehaviour
{
    private Animator animator;

    private SoundSystem soundSystem;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightFoot;
    [SerializeField] private GameObject leftFoot;
    [SerializeField] private GameObject body;
    private void Start()
    {
        soundSystem = SoundSystemUser.Instance;
        animator = GetComponent<Animator>();
        ITouchSystem inputSystem = FindObjectOfType<BodyController>();
        //ITouchSystem inputSystem = FindObjectOfType<BodyController>();
        inputSystem.OnTapped += HandleTap;
        //inputSystem.OnTapped += HandleTap;
    }

    private void HandleTap(Vector3 touchPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null) SelectAnimationMethod(hit.collider.gameObject);
    }

    private void SelectAnimationMethod(GameObject gameObject)
    {
        if (!amimationPlay())
        {
            if (gameObject.name == head.name) { PlayHeadAnimation(); }
            else if (gameObject.name == rightHand.name) { RightHandAnimation(); }
            else if (gameObject.name == leftHand.name) { LeftHandAnimation(); }
            else if (gameObject.name == rightFoot.name) { RightFootAnimation(); }
            else if (gameObject.name == leftFoot.name) { LeftFootAnimation(); }
            else if (gameObject.name == body.name) { PlayBodyAnimation(); }
        }
    }

    private void PlayHeadAnimation()
    {
        soundSystem.PlaySound("WipeFace");
        animator.SetBool("isHead" , true);
    }
    private void PlayBodyAnimation()
    {
        soundSystem.PlaySound("Joy");
        animator.SetBool("isBody", true);
    }

    private void RightHandAnimation()
    {
        soundSystem.PlaySound("ToLaughHand");
        animator.SetBool("isRightHand", true);
    }
    private void LeftHandAnimation()
    {
        soundSystem.PlaySound("ToLaughHand");
        animator.SetBool("isLeftHand", true);
    }
    private void LeftFootAnimation()
    {
        soundSystem.PlaySound("ToLaughFoot");
        animator.SetBool("isLeftFoot", true);
    }
    private void RightFootAnimation()
    {
        soundSystem.PlaySound("ToLaughFoot");
        animator.SetBool("isRightFoot", true);
    }

    public bool amimationPlay()
    {
        bool temp = false;
        temp |= animator.GetBool("isHead");
        temp |= animator.GetBool("isBody");
        temp |= animator.GetBool("isRightHand");
        temp |= animator.GetBool("isLeftHand");
        temp |= animator.GetBool("isLeftFoot");
        temp |= animator.GetBool("isRightFoot");
        temp |= animator.GetBool("isWaitAnimation");
        temp |= animator.GetBool("isMakeWave");
        temp |= animator.GetBool("isTaskCloud");
        return temp;
    }

    public void AnimEnd(string animation)
    {
        animator.SetBool(animation, false);
    }
}
