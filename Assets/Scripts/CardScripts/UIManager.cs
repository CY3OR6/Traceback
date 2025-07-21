using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField]
    GameObject continueButton = null;

    [SerializeField]
    GameObject menuPanel = null;
    [SerializeField]
    GameObject gridPanel = null;
    [SerializeField]
    GameObject scorePanel = null;
    [SerializeField]
    GameObject gameOverPanel = null;

    int score = 0;
    int combo = 0;
    int turns = 0;
    int matches = 0;
    int totalMatches = 0;

    int scoreToAdd = 10;

    [Header("Score Text Fields"), Space(15f)]
    [SerializeField]
    TMP_Text scoreText = null;
    [SerializeField]
    TMP_Text comboText = null;
    [SerializeField]
    TMP_Text turnsText = null;
    [SerializeField]
    TMP_Text matchesText = null;
    [SerializeField]
    TMP_Text totalMatchesText = null;

    [Header("GameOver Text Fields"), Space(15f)]
    [SerializeField]
    TMP_Text scoreGOText = null;
    [SerializeField]
    TMP_Text comboGOText = null;
    [SerializeField]
    TMP_Text turnsGOText = null;
    [SerializeField]
    TMP_Text matchesGOText = null;

    //Save names for stats

    public const string ScoreSaveName = "SavedScore";
    public const string ComboSaveName = "SavedCombo";
    public const string TurnsSaveName = "SavedTurns";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        menuPanel?.SetActive(true);
        gridPanel?.SetActive(false);
        scorePanel?.SetActive(false);
        gameOverPanel?.SetActive(false);


        if (PlayerPrefs.GetInt("NumberOfCards") > 0)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

    }

    private void Start()
    {

    }

    public void OnGameStart()
    {
        score = 0;
        combo = 0;
        turns = 0;
        matches = 0;

        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        turnsText.text = turns.ToString();
        matchesText.text = matches.ToString();

        menuPanel.SetActive(false);
        gridPanel.SetActive(true);
        scorePanel.SetActive(true);
        gameOverPanel?.SetActive(false);
    }

    public static void UpdateScore()
    {
        instance.combo++;
        instance.score += (instance.scoreToAdd * instance.combo);
        instance.scoreText.text = instance.score.ToString();
        instance.comboText.text = instance.combo.ToString();

        PlayerPrefs.SetInt(ScoreSaveName, instance.score);
        PlayerPrefs.SetInt(ComboSaveName, instance.combo);

    }

    public static void SetScore(int score, int combo)
    {
        instance.score = score;
        instance.scoreText.text = score.ToString();

        instance.combo = combo;
        instance.comboText.text = combo.ToString();
    }

    public static void ResetCombo()
    {
        instance.combo = 0;
        instance.comboText.text = instance.combo.ToString();
        PlayerPrefs.SetInt(ComboSaveName, instance.combo);
    }

    public static void UpdateTurns()
    {
        instance.turns++;
        instance.turnsText.text = instance.turns.ToString();
        PlayerPrefs.SetInt(TurnsSaveName, instance.turns);
    }

    public static void SetTurns(int turns)
    {
        instance.turns = turns;
        instance.turnsText.text = turns.ToString();
    }

    public static void UpdateMatches()
    {
        instance.matches++;
        instance.matchesText.text = instance.matches.ToString();

        if (instance.matches == instance.totalMatches)
        {
            Debug.Log("Game Over! You won!");
            instance.StartCoroutine(instance.OnGameOver());
        }

    }

    public static void SetMatches(int matches)
    {
        instance.matches = matches;
        instance.matchesText.text = matches.ToString();
    }

    public static void SetTotalMatches(int totalMatches)
    {
        instance.totalMatches = totalMatches;
        instance.totalMatchesText.text = $"/ {totalMatches}";
    }


    IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(1f);

        GameManager.ClearSave();

        gameOverPanel.SetActive(true);
        gridPanel.SetActive(false);
        scorePanel.SetActive(false);
        scoreGOText.text = $"Score: {score}";
        comboGOText.text = $"Combo: {combo}";
        turnsGOText.text = $"Turns: {turns}";
        matchesGOText.text = $"Matches: {matches} / {totalMatches}";
    }

}
