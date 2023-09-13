using UnityEngine;

public class Baby: MonoBehaviour
{
    public string Name { get; private set; }
    
    //head 
    [SerializeField] private SpriteRenderer _head;
    //haircut
    [SerializeField] private SpriteRenderer _hair;
    [SerializeField] private SpriteRenderer _rightPonytail;
    [SerializeField] private SpriteRenderer _leftPonytail;
    [SerializeField] private SpriteRenderer _hairBow;
    [SerializeField] private SpriteRenderer _elastic;
    [SerializeField] private SpriteRenderer _hairAccessory;
    //eyebrows
    [SerializeField] private SpriteRenderer _rightEyebrow;
    [SerializeField] private SpriteRenderer _leftEyebrow;
    //eyes
    [SerializeField] private SpriteRenderer _rightEyeball;
    [SerializeField] private SpriteRenderer _leftEyeball;
    [SerializeField] private SpriteRenderer _rightIris;
    [SerializeField] private SpriteRenderer _leftIris;
    [SerializeField] private SpriteRenderer _rightEyelid;
    [SerializeField] private SpriteRenderer _leftEyelid;
    //mouth
    [SerializeField] private SpriteRenderer _mouth;
    [SerializeField] private SpriteRenderer _tongue;

    //body
    [SerializeField] private SpriteRenderer _body;
    //arms
    [SerializeField] private SpriteRenderer _rightArm;
    [SerializeField] private SpriteRenderer _leftArm;
    //legs
    [SerializeField] private SpriteRenderer _rightLeg;
    [SerializeField] private SpriteRenderer _leftLeg;
    [SerializeField] private SpriteRenderer _rightFoot;
    [SerializeField] private SpriteRenderer _leftFoot;
    //head 
    public SpriteRenderer Head { private set { } get => _head; }
    //haircut
    public SpriteRenderer Hair => _hair;
    public SpriteRenderer RightPonytail => _rightPonytail;
    public SpriteRenderer LeftPonytail => _leftPonytail;
    public SpriteRenderer HairBow => _hairBow;
    public SpriteRenderer Elastic => _elastic;
    public SpriteRenderer HairAccessory => _hairAccessory;
    //eyebrows
    public SpriteRenderer RightEyebrow => _rightEyebrow;
    public SpriteRenderer LeftEyebrow => _leftEyebrow;
    //eyes
    public SpriteRenderer RightEyeball => _rightEyeball;
    public SpriteRenderer LeftEyeball => _leftEyeball;
    public SpriteRenderer RightIris => _rightIris;
    public SpriteRenderer LeftIris => _leftIris;
    public SpriteRenderer RightEyelid => _rightEyelid;
    public SpriteRenderer LeftEyelid => _leftEyelid;
    //mouth
    public SpriteRenderer Mouth => _mouth;
    public SpriteRenderer Tongue => _tongue;

    //body
    public SpriteRenderer Body => _body;
    //arms
    public SpriteRenderer RightArm => _rightArm;
    public SpriteRenderer LeftArm => _leftArm;
    //legs
    public SpriteRenderer RightLeg => _rightLeg;
    public SpriteRenderer LeftLeg => _leftLeg;
    public SpriteRenderer RightFoot => _rightFoot;
    public SpriteRenderer LeftFoot => _leftFoot;

    public void SetBaby(BabyChoiceController babyChoiceController)
    {
        //set head sprite
        Head.sprite = babyChoiceController.Head;

        //haircut
        Hair.sprite = babyChoiceController.Hair;
        // //set  RightPonytail sprite
        RightPonytail.sprite = babyChoiceController.RightPonytail;
        // //set LeftPonytail sprite
        LeftPonytail.sprite = babyChoiceController.LeftPonytail;
        // //set HairBow sprite
        HairBow.sprite = babyChoiceController.HairBow;
        // //set Elastic sprite
        Elastic.sprite = babyChoiceController.Elastic;
        // //set HairAccessory sprite
        HairAccessory.sprite = babyChoiceController.HairAccessory;

        //set RightEyebrow sprite
        RightEyebrow.sprite = babyChoiceController.Eyebrow;
        //set LeftEyebrow sprite
        LeftEyebrow.sprite = babyChoiceController.Eyebrow;

        //set RightEyeball sprite
        RightEyeball.sprite = babyChoiceController.RightEyeball;
        //set LeftEyeball sprite
        LeftEyeball.sprite = babyChoiceController.LeftEyeball;

        //set RightIris sprite
        RightIris.sprite = babyChoiceController.Iris;
        //set LeftIris sprite
        LeftIris.sprite = babyChoiceController.Iris;

        //set RightEyelid sprite
        RightEyelid.sprite = babyChoiceController.RightEyelid;
        //set LeftEyelid sprite
        LeftEyelid.sprite = babyChoiceController.LeftEyelid;

        //set Mouth sprite
        Mouth.sprite = babyChoiceController.Mouth;
        //set Tongue sprite
        Tongue.sprite = babyChoiceController.Tongue;

        //set Body sprite
        Body.sprite = babyChoiceController.Body;

        //set RightArm sprite
        RightArm.sprite = babyChoiceController.Arm;
        //set LeftArm sprite
        LeftArm.sprite = babyChoiceController.Arm;

        //set RightLeg sprite
        RightLeg.sprite = babyChoiceController.Leg;
        //set LeftLeg sprite
        LeftLeg.sprite = babyChoiceController.Leg;

        //set RightFoot sprite
        RightFoot.sprite = babyChoiceController.Foot;
        //set LeftFoot sprite
        LeftFoot.sprite = babyChoiceController.Foot;
    }
}
