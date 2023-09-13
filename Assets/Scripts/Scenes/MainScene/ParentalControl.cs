using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParentalControl : MonoBehaviour
{
    [SerializeField] private TMP_Text _taskTMP;
    [SerializeField] private TMP_Text[] _answerTMPs;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private Animator _animator;
    private string _parentalControlTrigger = "Hide";

    private int _answer;

    /// <summary>
    /// Прив'язує до кожної кнопки подію на перевірку відповіді
    /// </summary>
    private void Awake()
    {
        // "прив'язка" виклик перевірки відповіді до натиску на кнопку
        foreach (var button in _answerButtons)
        {
            button.onClick.AddListener(() => AnswerButtonClick(button));
        }
    }

    /// <summary>
    /// Видаляє подію з усіх кнопок
    /// </summary>
    private void OnDestroy()
    {
        foreach (var button in _answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
    
    /// <summary>
    /// Виклакається ф-ція "Task" після активації елементу
    /// </summary>
    private void OnEnable()
    {
        CreateTask();
    }
    
    /// <summary>
    /// Створює завдання
    /// </summary>
    private void CreateTask()
    {
        // діапазон можливих відповідів
        Vector2Int  answersRange;
        bool taskCoof = Random.Range(0, 2) == 0;
        if(taskCoof){
            answersRange =  CreateSumTask();
        }else{
            answersRange = CreateMultiplationTask();
        }

        CreateAnswers(answersRange.x,answersRange.y);
    }

    /// <summary>
    /// Створюємо завдання на додавання
    /// </summary>
    private Vector2Int CreateSumTask(){
        int firstNumber = Random.Range(1, 10);
        int secondNumber = Random.Range(1, 20);
        // обраховуєм суму та записуєм її як вірну відповідь
        _answer = firstNumber + secondNumber;
        // виводим текст дії завднання для гравця
        _taskTMP.text = $"{firstNumber} + {secondNumber} = ?";
        // повертаєм діапазон можливих відповідів
        return new Vector2Int(3, 30);
    }
    
    /// <summary>
    /// Створюємо завдання на множення
    /// </summary>
    private Vector2Int CreateMultiplationTask(){
        int firstNumber = Random.Range(2, 9);
        int secondNumber = Random.Range(2, 9);
        // обраховуєм множення та записуєм її як вірну відповідь
        _answer = firstNumber * secondNumber;
        _taskTMP.text = $"{firstNumber} * {secondNumber} = ?";
        // повертаєм діапазон можливих відповідів
        return new Vector2Int(5,99);
    }

    /// <summary>
    /// Вводимо мінімальну [minAnswer] та максимальну [maxAnswer] відповіді - 
    /// Присвоюємо на випадкову кнопку правильну відповідь і на решту кнопок неправильні відповіді
    /// </summary>
    private void CreateAnswers(int minAnswer, int maxAnswer)
    {
        // визначаєм індекс правильної відповіді
        int randomIndex = Random.Range(0, _answerTMPs.Length);
        // перераховуєм всі можливі кнопки
        for (int i = 0; i < _answerTMPs.Length; i++)
        {
            
            if (i == randomIndex)
            {
                // записуєм правильну відповідь у положення індексу
                _answerTMPs[i].text = _answer.ToString();
            }
            else
            {
                // генеруєм відповідь поки вона буде неправильна
                int randomAnswer = Random.Range(minAnswer, maxAnswer);
                while (randomAnswer == _answer)
                {
                    randomAnswer = Random.Range(minAnswer, maxAnswer);
                }
                // записуєм рандомну відповідь
                _answerTMPs[i].text = randomAnswer.ToString();
            }
        }
    }

    /// <summary>
    /// Вводимо кнопку [button] - перевіряє чи натиснута кнопка є відповіддю ? зникає панель з завданням : 
    /// генерує нове завдання 
    /// </summary>
    private void AnswerButtonClick(Button button)
    {
        // отримуєм текст функції
        TMP_Text answerText = button.GetComponentInChildren<TMP_Text>();
        int answer;
        // переводим string відповіді в int
        if (int.TryParse(answerText.text, out answer))
        {
            if (CheckAnswer(answer))
            {
                // якщо відповідь вірна активуєм панель налаштувань
                Debug.Log("Correct!");

                Hide();
            }
            else
            {
                // якщо відповідь не вірна створюєм нову
                CreateTask();
            }
        }
    }

    public void Hide()
    {
        StartCoroutine(HideParentConroll());
    }

    private IEnumerator HideParentConroll()
    {
        _animator.SetTrigger(_parentalControlTrigger);

        yield return new WaitUntil(() => AnimationChecker.Instance.IsAnimationOver(_animator, "Hide"));

        gameObject.SetActive(false);
        yield break;
    }

    /// <summary>
    /// Вводимо цілочисельне значення [answer] - повертаємо значення перевірки чи введене значення є вірним
    /// </summary>
    private bool CheckAnswer(int answer)
    {
        return _answer == answer;
    }
}
