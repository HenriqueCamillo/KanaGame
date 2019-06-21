using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que lida com toda a interface do menu principal
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Variáveis dos submenus
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private GameObject modeSelectPanel;
    [SerializeField] private GameObject deleteHiragana;
    [SerializeField] private GameObject deleteKatakana;
    [SerializeField] private GameObject credits;


    // Indica em qual menu o jogador está e qual silabário foi escolhido
    [SerializeField] private MenuState state;

    /// <summary>
    /// Enumerador que indica o estado atual (em qual menu o jogador está), para ser possível voltar entre os menus
    /// </summary>
    private enum MenuState
    {
        MainMenu,
        GameMenu,
        ModeSelectMenu,
        ProgressMenu,
        Credits
    }

    /// <summary>
    /// Desabilita todos os submenus e inicializa o valor do estado
    /// </summary>
    private void Start()
    {
        state = MenuState.MainMenu;
        gamePanel.SetActive(false);
        modeSelectPanel.SetActive(false);
        progressPanel.SetActive(false);
        deleteHiragana.SetActive(false);
        deleteKatakana.SetActive(false);
        credits.SetActive(false);
    }

    /// <summary>
    /// Abre o submenu de jogo
    /// </summary>
    public void Play()
    {
        gamePanel.SetActive(true);
        state = MenuState.GameMenu;
    }

    /// <summary>
    /// Seleciona o silabário e vai para o submenu de seleção de modo de jogo
    /// </summary>
    /// <param name="alphabet">Alfabeto escolhido</param>
    public void SyllabaryGame(string alphabet)
    {
        if (alphabet.Equals("Hiragana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Hiragana;
        else if (alphabet.Equals("Katakana"))
            GameManager.instance.alphabet = GameManager.Alphabet.Katakana;

        modeSelectPanel.SetActive(true);
        state = MenuState.ModeSelectMenu;
    }

    /// <summary>
    /// Abre o submenu de progresso do jogo
    /// </summary>
    public void ViewProgress()
    {
        progressPanel.SetActive(true);
        state = MenuState.ProgressMenu;
    }

    /// <summary>
    /// Volta para o estado anterior do menu
    /// </summary>
    public void Back()
    {
        // Dependendo de qual o menu atual, desativa o painel específico para voltar ao anterior, e atualiza o estado do meun
        switch (state)
        {
            case MenuState.GameMenu:
                gamePanel.SetActive(false);
                state = MenuState.MainMenu;
                break;   
            
            case MenuState.ModeSelectMenu:
                modeSelectPanel.SetActive(false);
                state = MenuState.GameMenu;
                break;

            case MenuState.ProgressMenu:
                progressPanel.SetActive(false);
                deleteHiragana.SetActive(false);
                deleteKatakana.SetActive(false);
                state = MenuState.MainMenu;
                break;
            
            case MenuState.Credits:
                credits.SetActive(false);
                state = MenuState.MainMenu;
                break;
        }
    }

    /// <summary>
    /// Mostra os créditos do jogo
    /// </summary>
    public void Credits()
    {
        credits.SetActive(true);
        state = MenuState.Credits;
    }

    /// <summary>
    /// Deleta o progresso de um alfabeto passado por parâmetro
    /// </summary>
    /// <param name="alphabet">Alfabeto que terá seu progresso resetado</param>
    public void ResetAlphabet(string alphabet)
    {
        KanaDictionary dictionary = null;

        if (alphabet.Equals("Hiragana"))
            dictionary = GameManager.instance.kanaDatabase.hiraganaStats;
        else if (alphabet.Equals("Katakana"))
            dictionary = GameManager.instance.kanaDatabase.katakanaStats;

        GameManager.instance.kanaDatabase.ResetHitsMisses(dictionary);
        CloseWarningMessage();
    }

    /// <summary>
    /// Mostra tela de aviso ao pedir para deletar progresso
    /// </summary>
    /// <param name="alphabet"></param>
    public void WarningMessage(string alphabet)
    {
        if (alphabet.Equals("Hiragana"))
            deleteHiragana.SetActive(true);
        else if (alphabet.Equals("Katakana"))
            deleteKatakana.SetActive(true);
    }

    /// <summary>
    /// Fecha a mensagem de aviso
    /// </summary>
    public void CloseWarningMessage()
    {
        deleteHiragana.SetActive(false);
        deleteKatakana.SetActive(false);
    }

    /// <summary>
    /// Sai do jogo
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}