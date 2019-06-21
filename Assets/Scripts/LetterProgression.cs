using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Classe responsável por mostrar o progresso de cada letra
/// </summary>
public class LetterProgression : MonoBehaviour
{
    // Variáveis de texto
    [SerializeField] private TextMeshProUGUI letter;
    [SerializeField] private TextMeshProUGUI reading; 
    [SerializeField] private TextMeshProUGUI fraction;
    [SerializeField] private TextMeshProUGUI percentage; 

    // Imagem da barra de porcentagem de acerto
    [SerializeField] private Image percentageSlider;

    /// <summary>
    /// Define o valor do texto como valor da variável passada por parâmetro
    /// </summary>
    /// <param name="letter">Letra do alfabeto japônes</param>
    public void SetLetter(string letter)
    {
        this.letter.text = letter;
    }

    /// <summary>
    /// Define o valor do texto como valor da variável passada por parâmetro
    /// </summary>
    /// <param name="reading">Leitura da letra do alfabeto japonês</param>
    public void SetReading(string reading)
    {
        this.reading.text = reading;
    }

    /// <summary>
    /// Define o valor do texto como valor da variável passada por parâmetro
    /// </summary>
    /// <param name="fraction">Fração de acertos por tentativas</param>
    public void SetFraction(string fraction)
    {
        this.fraction.text = fraction;
    }

    /// <summary>
    /// Define o tamanho da barra de porcentagem de acerto, e coloca o texto da porcetagem abaixo dela
    /// </summary>
    /// <param name="percentage"></param>
    public void SetPercentage(float percentage)
    {
        percentageSlider.fillAmount = percentage;
        this.percentage.text = (percentage * 100).ToString("0.0") + "%";
    }
}
