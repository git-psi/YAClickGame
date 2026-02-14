using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Mathematics;

public class TapCounter : MonoBehaviour
{
    [Header("UI References")]
    public Text timerText;
    public Text scoreText;
    public GameObject clickEffectPrefab;
    public RectTransform canvasRect;
    public GameObject menu;
    public GameObject game;

    [Header("Game Settings")]
    public float gameDuration = 10f;

    public Menu menuScript;

    private int score = 0;
    private float remainingTime;
    private bool gameActive = false;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!gameActive) return;

        // Detecte clics souris ou touch
        if (Input.GetMouseButtonDown(0)) // pour mobile et PC
        {
            Vector2 clickPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                null,
                out clickPos
            );

            OnTap(clickPos);
        }
    }

    public void StartGame()
    {
        score = 0;
        remainingTime = gameDuration;
        gameActive = true;
        scoreText.text = score.ToString();
        timerText.text = remainingTime.ToString("F1") + "s";
        StartCoroutine(GameTimer());
    }

    void OnTap(Vector2 position)
    {
        score++;
        scoreText.text = score.ToString();

        if (clickEffectPrefab != null)
        {
            GameObject effect = Instantiate(clickEffectPrefab, canvasRect);
            effect.GetComponent<RectTransform>().anchoredPosition = position;
        }
    }

    IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.Max(remainingTime, 0f).ToString("F1") + "s";
            yield return null;
        }

        gameActive = false;
        timerText.text = "Time's Up!";
        Debug.Log("Score final : " + score);

        StartCoroutine(GoToMenuDelay(2f));
    }

    IEnumerator GoToMenuDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GoToMenu(score);
    }

    void GoToMenu(int lastScore)
    {
        GameData.LastScore = lastScore;
        GameData.BestScore = math.max(lastScore, GameData.BestScore);
        GameData.Save();

        menuScript.SetMenuText();

        menu.SetActive(true);
        game.SetActive(false);
    }
}
