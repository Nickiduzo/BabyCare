using UnityEngine;


    [CreateAssetMenu(fileName = "BabyNationality", menuName = "Configs/BabyNationality")]
    public class BabyChoiceController : ScriptableObject
    {
        //head 
        [SerializeField] private Sprite _head;
        //haircut
        [SerializeField] private Sprite _hair;
        [SerializeField] private Sprite _rightPonytail;
        [SerializeField] private Sprite _leftPonytail;
        [SerializeField] private Sprite _hairBow;
        [SerializeField] private Sprite _elastic;
        [SerializeField] private Sprite _hairAccessory;
        //eyebrows
        [SerializeField] private Sprite _eyebrow;
        //eyes
        [SerializeField] private Sprite _rightEyeball;
        [SerializeField] private Sprite _leftEyeball;
        [SerializeField] private Sprite _iris;
        [SerializeField] private Sprite _rightEyelid;
        [SerializeField] private Sprite _leftEyelid;
        //mouth
        [SerializeField] private Sprite _mouth;
        [SerializeField] private Sprite _tongue;
        
        //body
        [SerializeField] private Sprite _body;
        //arms
        [SerializeField] private Sprite _arm;
        //legs
        [SerializeField] private Sprite _leg;
        [SerializeField] private Sprite _foot;

        //head 
        public Sprite Head => _head;
        //haircut
        public Sprite Hair => _hair;
        public Sprite RightPonytail => _rightPonytail;
        public Sprite LeftPonytail => _leftPonytail;
        public Sprite HairBow => _hairBow;
        public Sprite Elastic => _elastic;
        public Sprite HairAccessory => _hairAccessory;
        //eyebrows
        public Sprite Eyebrow=>_eyebrow;
        //eyes
        public Sprite RightEyeball => _rightEyeball;
        public Sprite LeftEyeball => _leftEyeball;
        public Sprite Iris => _iris;
        //Eyelids
        public Sprite RightEyelid => _rightEyelid;
        public Sprite LeftEyelid => _leftEyelid;
        //mouth
        public Sprite Mouth => _mouth;
        public Sprite Tongue => _tongue;

        //body
        public Sprite Body => _body;
        //arms
        public Sprite Arm => _arm;
        //legs
        public Sprite Leg => _leg;
        public Sprite Foot => _foot;
    }

