using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável por mostrar todo o progreso de cada silabário
/// </summary>
public class ProgressionBlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    private KanaDictionary currentAlphabet;


    /// <summary>
    /// Define o alfabeto
    /// </summary>
    private void Start()
    {
        if (GameManager.instance.alphabet == GameManager.Alphabet.Hiragana)
            currentAlphabet = GameManager.instance.kanaDatabase.hiraganaStats;
        else if (GameManager.instance.alphabet == GameManager.Alphabet.Katakana)
            currentAlphabet = GameManager.instance.kanaDatabase.katakanaStats;

        SpawnBlocks();
    }

    /// <summary>
    /// Cria blocos que mostram o progresso de cada letra do alfabeto
    /// </summary>
    void SpawnBlocks()
    {
        // Para cada caractere existente
        foreach (var element in currentAlphabet)
        {
            // Cria bloco na tela, e pega referência do script que possui os dados do bloco
            var instance = Instantiate(blockPrefab, this.transform.position, Quaternion.identity, this.transform);
            LetterProgression letterProgression = instance.GetComponent<LetterProgression>();

            // Coloca os valores da letra e sua leitura nos textos
            letterProgression.SetLetter(element.Key);
            letterProgression.SetReading(element.Value.Readings[0]);

            // Caso não haja nenhum caso especial
            if (element.Value.Appearences != 0)
            {
                // Cria string qeu representa fração de acertos por tentativas, e coloca no texto
                string fraction = (element.Value.Appearences - element.Value.Misses).ToString() + "/" + element.Value.Appearences.ToString();
                letterProgression.SetFraction(fraction);

                // Calcula porcentagem de acerto, e coloca no texto e no barra de porcentagem de acertos
                float percentage = (float)(element.Value.Appearences - element.Value.Misses) / (float)element.Value.Appearences;
                letterProgression.SetPercentage(percentage);
            }
            // Lida com o caso especial de a letra não ter aparecido, para não dividir por 0
            else
            {
                letterProgression.SetFraction("0/0");
                letterProgression.SetPercentage(0.0f);
            }
        }
    }
}
