using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField]
    GameObject MenuPanel = null;
    [SerializeField]
    GameObject GridPanel = null;
    [SerializeField]
    GameObject ScorePanel = null;

    int score = 0;
    int combo = 0;
    int turns = 0;
    int matches = 0;

    int scoreToAdd = 10;

    [Header("Text Fields"), Space(15f)]
    [SerializeField]
    TMP_Text scoreText = null;
    [SerializeField]
    TMP_Text comboText = null;
    [SerializeField]
    TMP_Text turnsText = null;
    [SerializeField]
    TMP_Text matchesText = null;

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

        MenuPanel?.SetActive(true);
        GridPanel?.SetActive(false);
        ScorePanel?.SetActive(false);

    }

    private void Start()
    {

    }

    public void OnGameStart()
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        turnsText.text = turns.ToString();
        matchesText.text = matches.ToString();

        MenuPanel.SetActive(false);
        GridPanel.SetActive(true);
        ScorePanel.SetActive(true);
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
    }

}
