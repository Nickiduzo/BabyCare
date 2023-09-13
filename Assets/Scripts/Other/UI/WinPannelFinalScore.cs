using TMPro;
using UnityEngine;

namespace UI
{
    public class WinPannelFinalScore : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public TextMeshProUGUI result;

        // for the appearance  Win Pannel Final Score
        public void Appear()
        {
            result.text = text.text;
        }
    }
}
