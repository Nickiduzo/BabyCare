using DG.Tweening;
using Quest;
using Scene;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UsefulComponents;

public class ChooseBaby : HudElement
{
    [SerializeField] private SaveBabyChoice _saveBabyChoice;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private AdsInterstitialconfigurator _ads;
    [SerializeField] private Animator _babyChoiceAnimator;
    [SerializeField] private AnimationClip _appearingClip;
    [SerializeField] private float _appearingBabiesDelay;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private Transform _hintStartPosition;
    [SerializeField] private Transform _hintEndPosition;

    [SerializeField] private FadeScreenPanel _fadeScreenPanel;

    [SerializeField] private GameObject[] _gameObjectsToDisable;
    [SerializeField] private Animator[] _babyAnimator;
    [SerializeField] private Transform[] _babyendPostion;
    [SerializeField] private GameObject[] _babyClouds;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject clouds;
    [SerializeField] private GameObject bird;
    [SerializeField] private Animator birdAnimator;

    private string _triggerName = "Appear";
    private Button[] _babyButtons;

    private GameObject Selectedbaby;

    private void Awake()
    {
        var buttonsCount = transform.childCount;
        _babyButtons = transform.GetComponentsInChildren<Button>();
    }

    private IEnumerator HintCoroutine()
    {
        ActivateHint(_hintStartPosition.position, _hintEndPosition.position);
        _particleSystem.Play();

        yield return new WaitForSeconds(5f);

        DisableHint();
    }
    private void ActivateHint(Vector3 startPosition, Vector3 endPosition)
    {
        HintSystem.Instance.ShowPointerHint(startPosition, endPosition);
        _particleSystem.transform.position = _hintEndPosition.position;
    }
    private void DisableHint()
    {
         HintSystem.Instance.HidePointerHint();
        _particleSystem.Stop();
        _particleSystem.Clear();
    }

    // play button press
    public void ChoiceOfBaby()
    {   
        CameraMoveUp();
    }

    private void DisableGameObjects()
    {
        foreach (GameObject go in _gameObjectsToDisable)
        {
            go.SetActive(false);
        }
    }

    // move camera to top position
    private void CameraMoveUp()
    {
        Sequence up = DOTween.Sequence();
        up.Append(camera.transform.DOMoveY(9, 2));
        up.Join(sun.transform.DOMoveY(sun.transform.position.y + 7, 2));
        up.Join(clouds.transform.DOMoveY(clouds.transform.position.y + 8, 2));
        up.AppendCallback(CameraInTopPosition);

        up.Play();
    }

    private void CameraInTopPosition() {

        StartCoroutine(BabiesAppearring());
      
    }
    //move babys to selection position
    private void moveBebyToPos()
    {
        for(int i  = 0; i < _babyAnimator.Length; i++)
        {
            _babyAnimator[i].gameObject.SetActive(true);
            _babyAnimator[i].gameObject.transform.DOMove(_babyendPostion[i].position , 1);
        }

    }

    private IEnumerator BabiesAppearring()
    {
        //_fadeScreenPanel.FadeIn();
       // yield return new WaitForSeconds(2);

        DisableGameObjects();

        // _fadeScreenPanel.FadeOut();

        // yield return new WaitForSeconds(_appearingBabiesDelay);

        // _babyChoiceAnimator.SetBool(_triggerName, true);
        moveBebyToPos();


        float wait = _appearingClip.length;
        yield return new WaitForSeconds(wait);

        foreach (Animator a in _babyAnimator)
        {
            a.SetBool("playHello", true);
        }
        StartCoroutine(HintCoroutine());

        for (int i = 0; i < _babyButtons.Length; i++)
        {
            _babyButtons[i].interactable = true;
        }

        yield break;
    }

    //get selected baby 
    public void SelectBaby(string babyName)
    {
        _saveBabyChoice.Save(babyName);
        switch (babyName)
        {
            case "Baby1":
                Selectedbaby = _babyAnimator[0].gameObject;
                break;
            case "Baby2":
                Selectedbaby = _babyAnimator[1].gameObject;
                break;
            case "Baby3":
                Selectedbaby = _babyAnimator[2].gameObject;
                break;
            case "Baby4":
                Selectedbaby = _babyAnimator[3].gameObject;
                break;
            case "Baby5":
                Selectedbaby = _babyAnimator[4].gameObject;
                break;
            case "Baby6":
                Selectedbaby = _babyAnimator[5].gameObject;
                break;

        }
        DisableHint();
        DisableNotSelectedBaby();
        // GoToQuest();
        birdAnimator.SetBool("move", true);
        MoveBabyAndBirdsToCenter();
    }
    //Disable Not Selected Baby
    private void DisableNotSelectedBaby()
    {
        for (int i = 0; i < _babyAnimator.Length; i++)
        {
            if(_babyAnimator[i].gameObject!= Selectedbaby)
            {
                _babyAnimator[i].gameObject.SetActive(false);
            }
            else
            {
                _babyClouds[i].SetActive(false);
            }
            _babyendPostion[i].gameObject.SetActive(false);
        }
    }

    //move selected baby and bird to center
    private void MoveBabyAndBirdsToCenter()
    {
        
        Sequence center = DOTween.Sequence();
        center.Append(Selectedbaby.transform.DOLocalMove(new Vector3(0,0,0), 1));
        center.Join(bird.transform.DOMoveY(bird.transform.position.y + 9.5f, 1));
        //center.Join(clouds.transform.DOMoveY(clouds.transform.position.y + 8, 2));
        center.AppendCallback(MoveBabyAndBirdsToDown);

        center.Play();

    }

    //move baby and bird down
    private void MoveBabyAndBirdsToDown()
    {
        
        Sequence down = DOTween.Sequence();
        down.PrependInterval(1);
        down.Append(Selectedbaby.transform.DOLocalMove(new Vector3(0, -16, 0), 3));
        down.Join(bird.transform.DOMoveY(bird.transform.position.y -16,3));
        down.Join(camera.transform.DOMoveY(0, 2));
        down.Join(sun.transform.DOMoveY(sun.transform.position.y - 7, 2));
        down.Join(clouds.transform.DOMoveY(clouds.transform.position.y - 8, 2));
        down.AppendCallback(GoToQuest);

        down.Play();

    }

    //  Go To Feeding for test
    private void GoToFeeding()
    {
        _soundSystem.PlaySound("PlaybuttonUIClick");
        Disappear();
        StartCoroutine(LoadScene(SceneType.Feeding));

    }
        /// <summary>
        /// Запускає процес переходу у сцену "QuestScene"
        /// </summary>
    private void GoToQuest()
    {

        _ads.StartInterstitialAd();
        _ads.interstitial.OnAdClosed.AddListener(LoadScene);
        _ads.interstitial.OnAdFailedToLoad.AddListener(LoadScene);
        _ads.interstitial.OnAdFailedToShow.AddListener(LoadScene);

       
    }
    public void LoadScene()
    {
        StartCoroutine(LoadScene(SceneType.Quest));
    }
    public void LoadScene(string  s)
    {
        StartCoroutine(LoadScene(SceneType.Quest));
    }


    private IEnumerator LoadScene(SceneType type)
    {
        yield return new WaitForSeconds(0);
        Debug.Log(type.ToString());
        _sceneLoader.LoadScene(type);
    }
}
