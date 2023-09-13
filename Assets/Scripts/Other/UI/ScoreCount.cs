using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace UI
{
    public class ScoreCount : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private int score = 0;
       public void AddScore(int addscore)
       {
            score += addscore;
            text.text = (score).ToString();
       }
        public void ResetScore()
        {
            score = 0;
            text.text = (score).ToString();
            
        }
    }
}
