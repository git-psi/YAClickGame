using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    public Text lastScoreText;
    public Text bestScoreText;
    public TapCounter tapCounter;


    private void Start()
    {
        SetMenuText();
    }

    public void StartGame()
    {
        game.SetActive(true);
        tapCounter.StartGame();

        menu.SetActive(false);
    }

    public void SetMenuText()
    {
        lastScoreText.text = GameData.LastScore.ToString();
        Debug.Log(GameData.LastScore);
        bestScoreText.text = GameData.BestScore.ToString();
        Debug.Log(GameData.BestScore);
    }
}
