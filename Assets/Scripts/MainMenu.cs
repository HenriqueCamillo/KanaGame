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
    [SerializeField] private GameObject syllabaryProgressPanel;


    // Indica em qual menu o jogador está e qual silabário foi escolhido
    [SerializeField] private MenuState state;
    [SerializeField] private Syllabary syllabary;


    /// <summary>
    /// Enumerador que indica qual está selecionado
    /// </summary>
    public enum Syllabary
    {
        Hiragana,
        Katakana
    }

    /// <summary>
    /// Enumerador que indica o estado atual (em qual menu o jogador está), para ser possível voltar entre os menus
    /// </summary>
    private enum MenuState
    {
        MainMenu,
        GameMenu,
        ModeSelectMenu,
        ProgressMenu,
        ShowingProgress

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
        syllabaryProgressPanel.SetActive(false);
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
    /// <param name="syllabary">Silabário escolhido</param>
    public void SyllabaryGame(string syllabary)
    {
        if (syllabary.Equals("Hiragana"))
            this.syllabary = Syllabary.Hiragana;
        else if (syllabary.Equals("Katakana"))
            this.syllabary = Syllabary.Katakana;

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
    /// Mostra o progresso atual do silabário escolhido
    /// </summary>
    /// <param name="syllabary">Silabário escolhido</param>
    public void ShowProgress(string syllabary)
    {
        if (syllabary.Equals("Hiragana"))
            this.syllabary = Syllabary.Hiragana;
        else if (syllabary.Equals("Katakana"))
            this.syllabary = Syllabary.Katakana;

        syllabaryProgressPanel.SetActive(true);
        state = MenuState.ShowingProgress;
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
                state = MenuState.MainMenu;
                break;

            case MenuState.ShowingProgress:
                syllabaryProgressPanel.SetActive(false);
                state = MenuState.ProgressMenu;
                break;
        }
    }

    /// <summary>
    /// Sai do jogo
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}