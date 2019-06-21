using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuessReadingGame : KanaGame
{
    [SerializeField]    TextMeshProUGUI showcaseKana;
    [SerializeField]    TextMeshProUGUI correctReadings;
    [SerializeField]    Image           inputFieldBoxImage;
    [SerializeField]    TMP_InputField  inputField;

    protected override void NextRound()
    {
        InvokeRepeating(nameof(FadeColorOut), 0f, Time.deltaTime);

        KeyValuePair<string, ReadingsAndStats> newKana;

        do      newKana         =   NextKana();
        while   (newKana.Key    ==  correctAnswer.Key);

        currentAlphabet[newKana.Key].Appearences++;
        correctReadings.text    =   "";
        showcaseKana.text       =   newKana.Key;
        correctAnswer           =   newKana;
        inputField.interactable =   true;
        inputField.ActivateInputField();
        inputField.Select();
    }

    protected override void Feedback(bool isCorrect)
    {
        inputField.interactable =   false;
        CancelInvoke(nameof(FadeColorOut));
        if(isCorrect == true)
        {
            inputFieldBoxImage.color = correctColor;
            Invoke(nameof(NextRound), correctFeedbackDuration);
        }
        else
        {
            inputFieldBoxImage.color = incorrectColor;
            Invoke(nameof(NextRound), incorrectFeedbackDuration);

            List<string> readings = currentAlphabet[showcaseKana.text].Readings;

            for(int i = 0; i < readings.Count - 1; i++)
                correctReadings.text += readings[i] + ", ";

            correctReadings.text += readings[readings.Count-1];
        }
    }

    protected override void FadeColorOut()
    {
        if(inputFieldBoxImage.color.Equals(Color.white))
            CancelInvoke(nameof(FadeColorOut));
        else
            inputFieldBoxImage.color = Color.Lerp(inputFieldBoxImage.color, Color.white, colorLerpSpeed);
    }

    protected override bool CompareAnswer(string answer)
    {
        return correctAnswer.Value.Readings.Contains(answer);
    }
}
