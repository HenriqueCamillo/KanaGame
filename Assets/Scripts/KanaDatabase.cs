using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "New KanaDataBase", menuName = "Kana/Database", order = 0)]
public class KanaDatabase : ScriptableObject
{
    public KanaDictionary hiraganaStats = new KanaDictionary();
    public KanaDictionary katakanaStats = new KanaDictionary();

    public void ResetHitsMisses()
    {
        foreach(var key in hiraganaStats.Keys)
        {
            hiraganaStats[key].Appearences  =   0;
            hiraganaStats[key].Misses       =   0;
        }
        
        foreach(var key in katakanaStats.Keys)
        {
            katakanaStats[key].Appearences  =   0;
            katakanaStats[key].Misses       =   0;
        }
    }

}
[System.Serializable]
public class KanaDictionary : SerializableDictionaryBase<string, ReadingsAndStats> {    }

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