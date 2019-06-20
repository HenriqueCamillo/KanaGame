using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Referência estática ao game manager
    public static GameManager instance;
    public KanaDatabase kanaDatabase;
    public KanaDictionary kanaDictionary;
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

    void Update()
    {
        
    }
}
