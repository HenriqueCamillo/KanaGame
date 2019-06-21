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
    [SerializeField]    float           randomKanaProbability;
    [SerializeField]    float           correctFeedbackDuration;
    [SerializeField]    float           incorrectFeedbackDuration;
    [SerializeField]    Color           correctColor;
    [SerializeField]    Color           incorrectColor;
    [SerializeField]    float           colorLerpSpeed;
    private             KanaDictionary  currentAlphabet;


    private void Start()
    {
        StartGame(GameManager.instance.alphabet.Hiragana);
    }

    public void StartGame(GameManager.Alphabet alphabet)
    {
        if(alphabet == GameManager.Alphabet.Hiragana)
            currentAlphabet = kanaDatabase.hiraganaStats;
        else if(alphabet == GameManager.Alphabet.Katakana)
            currentAlphabet = kanaDatabase.katakanaStats;
        
        NextKana();
        gamePanel.SetActive(true);
    }

    public void NextKana()
    {
        float spawnRatio;
        int randomIndex;
        bool hasFound = false;
        KeyValuePair<string, ReadingsAndStats> currentKana;
        var orderedDictionary = currentAlphabet.OrderByDescending(a => a.Value.Appearences != 0 ? (float)a.Value.Misses / (float)a.Value.Appearences : 0f);

        InvokeRepeating(nameof(FadeColorOut), 0f, Time.deltaTime);

        do
        {
            if(Random.Range(0f, 1f) <= randomKanaProbability)
            {
                hasFound                =   true;
                randomIndex             =   Random.Range(0, currentAlphabet.Count);
                currentKana             =   currentAlphabet.ElementAt(randomIndex);
            }
            else foreach(var kanaStats in orderedDictionary)
            {
                spawnRatio   =   kanaStats.Value.Appearences != 0 ? (float)kanaStats.Value.Misses / (float)kanaStats.Value.Appearences : 100;
                if(spawnRatio == 0)
                    spawnRatio = minimumProbability;
                else if(spawnRatio > maximumProbability)
                    spawnRatio = maximumProbability;

                if(showcaseKana.text != kanaStats.Key && Random.Range(0f, 1f) < spawnRatio)
                {
                    currentKana          =   kanaStats;
                    hasFound             =   true;
                    break;
                }
            }
        }
        while(hasFound == false);

        correctReadings.text    =   "";
        showcaseKana.text       =   currentKana.Key;
        currentAlphabet[currentKana.Key].Appearences++;
        inputField.interactable = true;
        inputField.ActivateInputField();
        inputField.Select();
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
