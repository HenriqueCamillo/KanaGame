using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

/// <summary>
///     Kana (Japanese alphabets) database. This class stores all the japanese characters and their readings to verify hits/misses from the player.
///     The actual values of the characters and readings we wanted in the game were filled in Unity by us.
/// </summary>
[CreateAssetMenu(fileName = "New KanaDataBase", menuName = "Kana/Database", order = 0)]
public class KanaDatabase : ScriptableObject  // The ScriptableObject inheritance makes it possible to store this database in a file
{
    public KanaDictionary hiraganaStats = new KanaDictionary();     // Hiragana is one of the japanese alphabets
    public KanaDictionary katakanaStats = new KanaDictionary();     // Katakana is also one of the japanese alphabets

    /// <summary>
    ///     Should the player want to reset this progress.
    /// </summary>
    public void ResetHitsMisses(KanaDictionary alphabet)
    {
        foreach(var key in alphabet.Keys)
        {
            alphabet[key].Appearences  =   0;
            alphabet[key].Misses       =   0;
        }
    }

}

/// <summary>
///     This is a custom dictionary that inherits from the default dictionary in C# but makes it possible for us to fill in the values in Unity's interface.
///     Other than that, it is a normal Dictionary with KeyValuePairs, Dictionary[Key] = Value, but the Value here is a custom class witch stores the values we need.static
/// </summary>
/// <typeparam name="string"></typeparam>
/// <typeparam name="ReadingsAndStats"></typeparam>
[System.Serializable]
public class KanaDictionary : SerializableDictionaryBase<string, ReadingsAndStats> {    }

/// <summary>
///     This is the custom class to be used with the dictionary, it stores the roman alphabet counterparts for the japanese characters, how many times that character has appeared and how many times the player got the reading wrong.
/// </summary>
[System.Serializable]
public class ReadingsAndStats
{
    public List<string> Readings;
    public int          Misses;
    public int          Appearences;

    public ReadingsAndStats(List<string> Readings, int Misses, int Appearences)
    {
        this.Readings       =   Readings;
        this.Misses         =   Misses;
        this.Appearences    =   Appearences;
    }
}