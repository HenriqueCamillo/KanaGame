using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
///     Essa classe é a classe do modo de jogo de escrita, que deriva da classe pai abstrata KanaGame, que contém referencias e funções comuns a ambos os modos de jogos.
///     O funcionamento é basicamente mostrar um caractere no topo da tela e esperar o usuário escrever a resposta e submeter para julgar se ela está correta, oferecendo
/// um feedback para o jogador e a resposta correta em caso de erro. E repete.
/// </summary>
public class GuessReadingGame : KanaGame
{
    [SerializeField]    TextMeshProUGUI showcaseKana;
    [SerializeField]    TextMeshProUGUI correctReadings;
    [SerializeField]    Image           inputFieldBoxImage;
    [SerializeField]    TMP_InputField  inputField;

    /// <summary>
    ///     A rodada desse modo de jogo da escrita é básicamente gerar uma nova sílaba para ser exibida (evitando ser a mesma da anterior) e aguardar o input do jogador.
    /// </summary>
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
        inputField.text = "";                                       
        inputField.ActivateInputField();
    }

    /// <summary>
    ///     O feedback nesse modo de jogo é basicamente mostrar a cor verde ou vermelha dependendo se o jogador acertou ou não a leitura da sílaba, com um delay maior para
    /// quando o jogador errou, para poder ver as leituras corretas que são exibidas em baixo da sílaba ao final.
    /// </summary>
    /// <param name="isCorrect">    Se a resposta foi correta ou não. (Chamada pela função answer, vide função pai KanaGame.    </param>
    protected override void Feedback(bool isCorrect)
    {
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

    /// <summary>
    ///     Função assíncrona que faz com que a cor da caixa de input volte à cor normal. Interpola a cor atual com a cor branca (base) até que fique igual.
    /// </summary>
    protected override void FadeColorOut()
    {
        if(inputFieldBoxImage.color.Equals(Color.white))
            CancelInvoke(nameof(FadeColorOut));
        else
            inputFieldBoxImage.color = Color.Lerp(inputFieldBoxImage.color, Color.white, colorLerpSpeed);
    }

    /// <summary>
    ///     Função de comparação a ser utilizada pela função Answer (Olhar classe pai KanaGame) que verifica se a resposta é uma leitura válida daquela sílaba.
    /// </summary>
    /// <param name="answer">   String da resposta do jogador, obtida pela caixa de input do teclado.   </param>
    /// <returns></returns>
    protected override bool CompareAnswer(string answer)
    {
        return correctAnswer.Value.Readings.Contains(answer);
    }
}
