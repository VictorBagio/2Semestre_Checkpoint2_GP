using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("HUD")]
    public TMP_Text coinText;
    public TMP_Text timerText;
    public GameObject gameplayUI;
    
    [Header("Tela Final")]
    public GameObject victoryPanel;
    public TMP_Text finalCoinText;
    public TMP_Text finalTimeText;
    

    private int coins = 0;
    private float runTime = 0f;

    private bool runFinished = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        victoryPanel.SetActive(false);

        UpdateCoinUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (!runFinished)
        {
            runTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = coins.ToString();
    }

    void UpdateTimerUI()
    {
        timerText.text = FormatTime(runTime);
    }

    public void FinishRun()
    {
        if (runFinished)
            return;

        runFinished = true;

        finalCoinText.text = coins.ToString();
        finalTimeText.text = FormatTime(runTime);

        gameplayUI.SetActive(false);
        victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return string.Format(
            "{0:00}:{1:00}.{2:000}",
            minutes,
            seconds,
            milliseconds
        );
    }
}