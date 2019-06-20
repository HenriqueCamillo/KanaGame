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
    /// Vai para o modo de jogo de indentificação de hiragana
    /// </summary>
    public void IndentifyHiragana()
    {

    }

    /// <summary>
    /// Vai para o modo de jogo de escrita de hiragana
    /// </summary>
    public void WriteHiragana()
    {

    }

    /// <summary>
    /// Vai para o modo de jogo de indentificação de katakana
    /// </summary>
    public void IndentifyKatakana()
    {

    }

    /// <summary>
    /// Vai para o modo de jogo de escrita de katakana
    /// </summary>
    public void WriteKatakana()
    {

    }

    /// <summary>
    /// Vai para a tela de mostrar progresso de hiragana
    /// </summary>
    public void ViewHiraganaProgress()
    {

    }

    /// <summary>
    /// Vai para a tela de mostrar progresso de hiragana
    /// </summary>
    public void ViewKatakanaProgress()
    {

    }

    /// <summary>
    /// Vai para o menu principal, se já não estiver nele
    /// </summary>
    public void Menu()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
            SceneManager.LoadScene("Menu");
    }
}
