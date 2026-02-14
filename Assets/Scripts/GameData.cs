using UnityEngine;

public static class GameData
{
    private const string LAST_SCORE_KEY = "LastScore";
    private const string BEST_SCORE_KEY = "BestScore";

    // LastScore
    public static int LastScore
    {
        get => PlayerPrefs.GetInt(LAST_SCORE_KEY, 0); // 0 si pas encore défini
        set => PlayerPrefs.SetInt(LAST_SCORE_KEY, value);
    }

    // BestScore
    public static int BestScore
    {
        get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        set
        {
            if (value > BestScore)
            {
                PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
            }
        }
    }

    // Sauvegarder les changements (pas toujours nécessaire mais recommandé)
    public static void Save()
    {
        PlayerPrefs.Save();
    }
}