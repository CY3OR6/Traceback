using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

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
    }

    public static void ResetCombo()
    {
        instance.combo = 0;
        instance.comboText.text = instance.combo.ToString();
    }

    public static void UpdateTurns()
    {
        instance.turns++;
        instance.turnsText.text = instance.turns.ToString();
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

    public static void SetTotalMatches(int totalMatches)
    {
        instance.totalMatches = totalMatches;
        instance.totalMatchesText.text = "/ " + totalMatches.ToString();
    }

    IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(1f);

        gameOverPanel.SetActive(true);
        gridPanel.SetActive(false);
        scorePanel.SetActive(false);
        scoreGOText.text = "Score: " + score.ToString();
        comboGOText.text = "Combo: " + combo.ToString();
        turnsGOText.text = "Turns: " + turns.ToString();
        matchesGOText.text = "Matches: " + matches.ToString() + "/ " + totalMatches.ToString();
    }

}
