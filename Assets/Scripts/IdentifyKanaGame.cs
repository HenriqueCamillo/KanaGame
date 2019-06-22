using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

/// <summary>
///     Jogo de identificação, deixa a leitura de um caractere exposta e espera que o jogador selecione a resposta que contém o caractere em si.
///     Basicamente é gerado caracteres para cada opção, evitando repetições, e aguarda que o jogador clique em alguma delas.
/// </summary>
public class IdentifyKanaGame : KanaGame
{
    [SerializeField]    TextMeshProUGUI     showcaseReading;
    [SerializeField]    AnswerOption[]      answerOptions;
    private             AnswerOption        selectedOption;

    /// <summary>
    ///     Define uma rodada do modo de jogo de identificação. Isto é: Gerar um caractere para ser questionado e um para cada uma das opções, evitando repetições.
    /// </summary>
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

    /// <summary>
    ///     Função que lida com o click de um botão de resposta, mandando a string daquela opção para a função answer.
    /// </summary>
    /// <param name="option">   Botão selecionado.  </param>
    public void SelectOption(AnswerOption option)
    {
        selectedOption  =   option;
        Answer(option.Kana.text);
    }

    /// <summary>
    ///     Feedback do modo de jogo de identificação, deixa verde o botão da resposta correta e vermelho os das incorretas.
    ///     Deixa por mais tempo quando é incorreta, para o jogador entender direito.
    /// </summary>
    /// <param name="isCorrect"></param>
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

    /// <summary>
    ///     Volta as cores do feedback ao normal.
    /// </summary>
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

    /// <summary>
    ///     Verifica se a silaba selecionada na resposta tem a leitura indicada.
    /// </summary>
    /// <param name="answer">   String da resposta. </param>
    /// <returns></returns>
    protected override bool CompareAnswer(string answer)
    {
        return correctAnswer.Key.Equals(answer);
    }
}