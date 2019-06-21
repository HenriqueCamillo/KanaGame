using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class IdentifyKanaGame : KanaGame
{
    [SerializeField]    TextMeshProUGUI     showcaseReading;
    [SerializeField]    AnswerOption[]      answerOptions;
    private             AnswerOption        selectedOption;

    protected override void NextRound()
    {
        InvokeRepeating(nameof(FadeColorOut), 0f, Time.deltaTime);

        AnswerOption[]              shuffledOptions;
        System.Random rng       =   new System.Random();
        shuffledOptions         =   answerOptions.OrderBy(a => rng.Next()).ToArray();   // Embaralha a lista aleatoriamente

        KeyValuePair<string, ReadingsAndStats>  newKana;
        do      newKana         =   NextKana();
        while   (newKana.Key    ==  correctAnswer.Key);

        correctAnswer           =   newKana;
        showcaseReading.text    =   newKana.Value.Readings[0];
        currentAlphabet[newKana.Key].Appearences++;

        foreach(var option in answerOptions)
            option.Kana.text    =   "";

        shuffledOptions[0].Kana.text    =   newKana.Key;
        for(int i = 1; i < shuffledOptions.Length; i++)
        {
            do      newKana     =   NextKana();
            while   (shuffledOptions.Where(option => option.Kana.text == newKana.Key).Count() > 0);
            shuffledOptions[i].Kana.text    =   newKana.Key;
        }

        foreach(var option in answerOptions)
            option.Button.interactable = true;
    }

    public void SelectOption(AnswerOption option)
    {
        selectedOption  =   option;
        Answer(option.Kana.text);
    }

    protected override void Feedback(bool isCorrect)
    {
        CancelInvoke(nameof(FadeColorOut));
        foreach(var option in answerOptions)
        {
            option.Button.interactable  =   false;

            if(option.Kana.text         ==  correctAnswer.Key)
                option.Box.color        =   correctColor;
            else
                option.Box.color        =   incorrectColor;
        }

        
        if(isCorrect == true)
            Invoke(nameof(NextRound), correctFeedbackDuration);
        else
            Invoke(nameof(NextRound), incorrectFeedbackDuration);
    }

    protected override void FadeColorOut()
    {
        foreach(var option in answerOptions)
        {
            if(option.Box.color.Equals(Color.white))
                CancelInvoke(nameof(FadeColorOut));
            else
                option.Box.color = Color.Lerp(option.Box.color, Color.white, colorLerpSpeed);
        }
    }

    protected override bool CompareAnswer(string answer)
    {
        return correctAnswer.Key.Equals(answer);
    }
}
