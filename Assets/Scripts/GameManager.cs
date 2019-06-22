using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe com referência estática a ela mesma (singleton) para que todos possam acessar
/// os atributos dela. Armazena qual o alfabeto escolhido, e os dicinários dos dois alfabetos
/// </summary>
public class GameManager : MonoBehaviour
{
    // Referência estática ao game manager
    public static GameManager instance;
    public KanaDatabase kanaDatabase;
    public Alphabet alphabet;

    /// <summary>
    /// Enumerador que indica qual está selecionado
    /// </summary>
    public enum Alphabet
    {
        Hiragana,
        Katakana
    }

    /// <summary>
    /// Verifica condição do singleton
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
