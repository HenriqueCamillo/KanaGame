using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class KanaGame : MonoBehaviour
{
    [SerializeField]    protected   KanaDatabase            kanaDatabase;
    [SerializeField]    protected   GameObject              gamePanel;
    [SerializeField]    protected   float                   minimumProbability;
    [SerializeField]    protected   float                   maximumProbability;
    [SerializeField]    protected   float                   randomKanaProbability;
    [SerializeField]    protected   float                   correctFeedbackDuration;
    [SerializeField]    protected   float                   incorrectFeedbackDuration;
    [SerializeField]    protected   Color                   correctColor;
    [SerializeField]    protected   Color                   incorrectColor;
    [SerializeField]    protected   float                   colorLerpSpeed;
    [SerializeField]    protected   bool                    isDebugMode;
    [SerializeField]    protected   GameManager.Alphabet    debugAlphabet;
    protected                       KanaDictionary          currentAlphabet;
    protected  KeyValuePair<string, ReadingsAndStats>       correctAnswer;         

    protected void Start()
    {
        gamePanel.SetActive(false);

        if(isDebugMode == false)
            StartGame(GameManager.instance.alphabet);
        else
            StartGame(debugAlphabet);
    }

    protected virtual void StartGame(GameManager.Alphabet alphabet)
    {
        if(alphabet == GameManager.Alphabet.Hiragana)
            currentAlphabet = kanaDatabase.hiraganaStats;
        else if(alphabet == GameManager.Alphabet.Katakana)
            currentAlphabet = kanaDatabase.katakanaStats;

        NextRound();
        gamePanel.SetActive(true);
    }

    protected abstract void NextRound();

    protected virtual KeyValuePair<string, ReadingsAndStats> NextKana()
    {
        float spawnRatio;
        int randomIndex;
        bool hasFound = false;
        KeyValuePair<string, ReadingsAndStats> newKana;
        var orderedDictionary = currentAlphabet.OrderByDescending(a => a.Value.Appearences != 0 ? (float)a.Value.Misses / (float)a.Value.Appearences : 0f);

        do
        {
            if(Random.Range(0f, 1f) <= randomKanaProbability)
            {
                randomIndex             =   Random.Range(0, currentAlphabet.Count);
                newKana                 =   currentAlphabet.ElementAt(randomIndex);
                hasFound            =   true;
            }
            else foreach(var kanaStats in orderedDictionary)
            {
                spawnRatio   =   kanaStats.Value.Appearences != 0 ? (float)kanaStats.Value.Misses / (float)kanaStats.Value.Appearences : 100;
                if(spawnRatio == 0)
                    spawnRatio = minimumProbability;
                else if(spawnRatio > maximumProbability)
                    spawnRatio = maximumProbability;

                if(Random.Range(0f, 1f) < spawnRatio)
                {
                    newKana             =   kanaStats;
                    hasFound            =   true;
                    break;
                }
            }
        }
        while(hasFound == false);

        return newKana;
    }

    public void Answer(string answer)
    {
        answer = answer.ToLower();

        if(CompareAnswer(answer))
            Feedback(true);
        else
        {
            currentAlphabet[correctAnswer.Key].Misses++;
            Feedback(false);
        }
    }
    
    protected abstract bool CompareAnswer(string answer);

    protected abstract void Feedback(bool isCorrect);

    protected abstract void FadeColorOut();
}
