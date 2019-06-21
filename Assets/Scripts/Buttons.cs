using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que possui as definições dos métodos chamados ao clicar em certos botões no menu
/// </summary>
public class Buttons : MonoBehaviour
{

    /// <summary>
    /// Mostra o progresso atual do silabário escolhido
    /// </summary>
    /// <param name="alphabet">Silabário escolhido</param>
    public void ShowProgress(string alphabet)
    {
        if (alphabet.Equals("Hiragana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Hiragana;
        else if (alphabet.Equals("Katakana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Katakana;

        SceneManager.LoadScene("Progress");
    }

    /// <summary>
    /// Vai para o modo de jogo de indentificação
    /// </summary>
    /// <param name="alphabet">Silabário escolhido</param>
    public void Indentify(string alphabet)
    {
        if (alphabet.Equals("Hiragana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Hiragana;
        else if (alphabet.Equals("Katakana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Katakana;

        SceneManager.LoadScene("Identify");

    }

    /// <summary>
    /// Vai para o modo de jogo de escrita 
    /// </summary>
    /// <param name="alphabet">Silabário escolhido</param>
    public void Write(string alphabet)
    {
        if (alphabet.Equals("Hiragana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Hiragana;
        else if (alphabet.Equals("Katakana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Katakana;

        SceneManager.LoadScene("Write");

    }

    /// <summary>
    /// Vai para o menu principal, se já não estiver nele
    /// </summary>
    public void Menu()
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
