using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Classe base para ambos os modos de jogo, contém a função de gerar um novo caractere que leva em consideração quantas vezes o usuário errou esses caracteres
/// para mostrar eles com mais frequência, mas ainda sim com um toque de aleatoriedade para aparecer caracteres novos.
///     Além disso requer funções obrigatórias nas classes filhas e possui uma função genérica de processar a resposta do usuário.
/// </summary>
public abstract class KanaGame : MonoBehaviour
{
    [SerializeField]    protected   KanaDatabase            kanaDatabase;
    [SerializeField]    protected   GameObject              gamePanel;
    [SerializeField]    protected   float                   minimumProbability;
    [SerializeField]    protected   float                   maximumProbability;
    [SerializeField]    protected   float                   randomKanaProbability;
    [SerializeField]    protected   float                   correctFeedbackDuration;
    [SerializeField]    protected   float                   incorrectFeedbackDuration;
    [SerializeField]    protected   Color                   correctColor;
    [SerializeField]    protected   Color                   incorrectColor;
    [SerializeField]    protected   float                   colorLerpSpeed;
    protected                       KanaDictionary          currentAlphabet;
    protected  KeyValuePair<string, ReadingsAndStats>       correctAnswer;         

    /// <summary>
    ///     Comeca o jogo com o alfabeto selecionado pelo jogador, que está armazenado no GameManager, um singleton que persiste entre trocas de cena.
    /// </summary>
    protected void Start()
    {
        gamePanel.SetActive(false);

        StartGame(GameManager.instance.alphabet);
    }

    /// <summary>
    ///     Função para começar a lógica do jogo, chama o primeiro round do jogo e faz os elementos serem visíveis assim que ele termina de carregar.
    /// </summary>
    /// <param name="alphabet">     Qual alfabeto será utilizado: Hiragana ou Katakana.      </param>
    protected virtual void StartGame(GameManager.Alphabet alphabet)
    {
        if(alphabet == GameManager.Alphabet.Hiragana)
            currentAlphabet = kanaDatabase.hiraganaStats;
        else if(alphabet == GameManager.Alphabet.Katakana)
            currentAlphabet = kanaDatabase.katakanaStats;

        NextRound();
        gamePanel.SetActive(true);
    }

    /// <summary>
    ///     Função genérica para um "round" do jogo, isso é, uma iteração de pergunta-resposta ao usuário.
    /// </summary>
    protected abstract void NextRound();

    /// <summary>
    ///     Obtem um novo caractere para ser usado nos modos de jogo. Faz isso dinamicamente: 
    ///     Existe uma porcentagem de chance do caractere escolhido ser aleatório (60%-70%), caso esse teste falhe, o dicionário de caracteres é ordenado de forma 
    /// decrescente por ordem de porcentagem de erros (Erros/Aparições) e é feito outro teste para cada caractere, sendo a porcentagem de chance a própria porcentagem
    /// de erros, com um limite máximo de (15%-30%) e mínimo (1%-5%). Dessa forma conseguimos fazer sílabas que o jogador tem dificuldade aparecerem com maior frequência
    /// mas não fazer com que só o mesmo conjunto seleto de sílabas apareça sempre.
    /// </summary>
    /// <returns>   Par do dicionario de Kanas, contém o caractere e as informações das leituras e dos acertos/erros do usuário em relação àquele caractere.false   </returns>
    protected virtual KeyValuePair<string, ReadingsAndStats> NextKana()
    {
        float spawnRatio;
        int randomIndex;
        bool hasFound = false;
        KeyValuePair<string, ReadingsAndStats> newKana;
        var orderedDictionary = currentAlphabet.OrderByDescending(a => a.Value.Appearences != 0 ? (float)a.Value.Misses / (float)a.Value.Appearences : 0f);

        do
        {
            if(Random.Range(0f, 1f) <= randomKanaProbability)
            {
                randomIndex             =   Random.Range(0, currentAlphabet.Count);
                newKana                 =   currentAlphabet.ElementAt(randomIndex);
                hasFound            =   true;
            }
            else foreach(var kanaStats in orderedDictionary)
            {
                spawnRatio   =   kanaStats.Value.Appearences != 0 ? (float)kanaStats.Value.Misses / (float)kanaStats.Value.Appearences : 100;
                if(spawnRatio == 0)
                    spawnRatio = minimumProbability;
                else if(spawnRatio > maximumProbability)
                    spawnRatio = maximumProbability;

                if(Random.Range(0f, 1f) < spawnRatio)
                {
                    newKana             =   kanaStats;
                    hasFound            =   true;
                    break;
                }
            }
        }
        while(hasFound == false);

        return newKana;
    }

    /// <summary>
    ///     Verifica se a resposta do usuário está correta e chama a função de Feedback.
    /// </summary>
    /// <param name="answer"></param>
    public void Answer(string answer)
    {
        answer = answer.ToLower();

        if(CompareAnswer(answer))
            Feedback(true);
        else
        {
            currentAlphabet[correctAnswer.Key].Misses++;
            Feedback(false);
        }
    }
    
    /// <summary>
    ///     Função genérica para comparação da resposta, já que cada modo de jogo compara uma coisa diferente.
    /// </summary>
    /// <param name="answer">   String da resposta do jogador.  </param>
    /// <returns></returns>
    protected abstract bool CompareAnswer(string answer);

    /// <summary>
    ///     Função genérica para o feedback da resposta do jogador.
    /// </summary>
    /// <param name="isCorrect"></param>
    protected abstract void Feedback(bool isCorrect);

    /// <summary>
    ///     Função genérica para voltar as cores ao normal após um feedback.
    /// </summary>
    protected abstract void FadeColorOut();
}
