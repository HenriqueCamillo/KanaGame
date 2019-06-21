using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Escolhe a fonte dependendo do alfabeto
/// </summary>
public class FontChooser : MonoBehaviour
{
    [SerializeField] TMP_FontAsset hiragana, katakana;
    [SerializeField] TextMeshProUGUI letter;

    /// <summary>
    ///  Verifica o silabário no game manager e escolhe a fonte
    /// </summary>
    void Start()
    {
        if (GameManager.instance.alphabet == GameManager.Alphabet.Katakana)
            letter.font = katakana;
        else
            letter.font = hiragana;
    }
}
