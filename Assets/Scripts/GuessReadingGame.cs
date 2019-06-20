using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

public class GuessReadingGame : MonoBehaviour
{
    [SerializeField]    TextMeshProUGUI showcaseKana;
    [SerializeField]    TextMeshProUGUI correctReadings;
    [SerializeField]    Image           inputFieldBoxImage;
    [SerializeField]    TMP_InputField  inputField;
    [SerializeField]    KanaDatabase    kanaDatabase;
    [SerializeField]    GameObject      gamePanel;
    [SerializeField]    float           minimumProbability;
    [SerializeField]    float           maximumProbability;
    [SerializeField]    float           correctFeedbackDuration;
    [SerializeField]    float           incorrectFeedbackDuration;
    [SerializeField]    Color           correctColor;
    [SerializeField]    Color           incorrectColor;
    [SerializeField]    float           colorLerpSpeed;
    private             KanaDictionary  currentAlphabet;

    public enum Alphabet { Hiragana, Katakana };

    private void Start()
    {
        StartGame(Alphabet.Hiragana);
    }

    public void StartGame(Alphabet alphabet)
    {
        if(alphabet == Alphabet.Hiragana)
            currentAlphabet = kanaDatabase.hiraganaStats;
        else if(alphabet == Alphabet.Katakana)
            currentAlphabet = kanaDatabase.katakanaStats;
        
        inputField.interactable = false;
        NextKana();
        gamePanel.SetActive(true);
    }

    public void NextKana()
    {
        float missRatio;
        bool hasFound = false;
        var orderedDictionary = currentAlphabet.OrderByDescending(a => a.Value.Appearences != 0 ? (float)a.Value.Misses / (float)a.Value.Appearences : 0f);

        InvokeRepeating(nameof(FadeColorOut), 0f, Time.deltaTime);

        do
        {
            foreach(var currentElement in orderedDictionary)
            {
                missRatio   =   currentElement.Value.Appearences != 0 ? (float)currentElement.Value.Misses / (float)currentElement.Value.Appearences : 0;

                if(missRatio == 0)
                    missRatio = minimumProbability;
                else if(missRatio > maximumProbability)
                    missRatio = maximumProbability;

                if(Random.Range(0f, 1f) <= missRatio)
                {
                    hasFound = true;
                    correctReadings.text    =   "";
                    showcaseKana.text       =   currentElement.Key;
                    inputField.interactable =   true;
                    currentAlphabet[currentElement.Key].Appearences++;
                    inputField.ActivateInputField();
                    inputField.Select();
                    return;
                }
            }
        }
        while(hasFound == false);
    }

    public void Answer(string answer)
    {
        inputField.interactable = false;
        ReadingsAndStats correctAnswerData = currentAlphabet[showcaseKana.text];
        answer = answer.ToLower();

        if(correctAnswerData.Readings.Contains(answer))
            Feedback(true);
        else
        {
            correctAnswerData.Misses++;
            Feedback(false);
        }
    }

    public void Feedback(bool isCorrect)
    {
        CancelInvoke(nameof(FadeColorOut));
        if(isCorrect == true)
        {
            inputFieldBoxImage.color = correctColor;
            Invoke(nameof(NextKana), correctFeedbackDuration);
        }
        else
        {
            inputFieldBoxImage.color = incorrectColor;
            Invoke(nameof(NextKana), incorrectFeedbackDuration);

            List<string> readings = currentAlphabet[showcaseKana.text].Readings;

            for(int i = 0; i < readings.Count - 1; i++)
                correctReadings.text += readings[i] + ", ";

            correctReadings.text += readings[readings.Count-1];
        }
    }

    public void FadeColorOut()
    {
        if(inputFieldBoxImage.color.Equals(Color.white))
            CancelInvoke(nameof(FadeColorOut));
        else
            inputFieldBoxImage.color = Color.Lerp(inputFieldBoxImage.color, Color.white, colorLerpSpeed);
    }
}
