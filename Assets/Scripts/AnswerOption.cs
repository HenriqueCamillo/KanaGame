using UnityEngine;
using UnityEngine.UI;
using TMPro;    

    public class AnswerOption : MonoBehaviour
    {
        [SerializeField] IdentifyKanaGame identifyKanaGame;

        public  TextMeshProUGUI Kana    { get; set; }
        public  Button          Button  { get; set; }
        public  Image           Box     { get; set; }

        private void Awake()
        {
            Button  =   GetComponent<Button>();
            Box     =   GetComponent<Image>();
            Kana    =   GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SubmitAnswer()
        {
            identifyKanaGame.SelectOption(this);
        }
    }