using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField]
    List<string> cardIDs = new List<string>();

    List<CardController> cards = new List<CardController>();

    [SerializeField]
    GameObject cardPrefab = null;

    [SerializeField]
    Transform cardParent = null;

    [SerializeField]
    CardController cardSelected1 = null;

    [SerializeField]
    CardController cardSelected2 = null;

    public UnityEvent OnGameStart = null;

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

    }

    private void Start()
    {

    }

    public void SetupNewGame()
    {
        OnGameStart?.Invoke();

        if (cards.Count > 0)
        {
            foreach (CardController card in cards)
            {
                Destroy(card.gameObject);
            }
            cards.Clear();
        }

        List<string> _tempCardId = new List<string>(cardIDs);

        for (int i = cardIDs.Count - 1; i >= 0; i--)
        {
            _tempCardId.Add(cardIDs[i]);
        }

        UIManager.SetTotalMatches(_tempCardId.Count / 2);

        int ri = Random.Range(0, _tempCardId.Count);

        while (_tempCardId.Count > 0)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            CardController cardController = newCard.GetComponent<CardController>();
            cardController.SetCardID(_tempCardId[ri]);
            cards.Add(cardController);
            _tempCardId.RemoveAt(ri);
            ri = Random.Range(0, _tempCardId.Count);
        }

        PlayerPrefs.SetInt("NumberOfCards", cards.Count);
        SaveCards();
    }

    public static void SelectCard(CardController card)
    {
        if (instance.cardSelected1 == null)
        {
            instance.cardSelected1 = card;
        }
        else if (instance.cardSelected2 == null && instance.cardSelected1 != card)
        {
            instance.cardSelected2 = card;
        }

        if (instance.cardSelected1 != null && instance.cardSelected2 != null)
        {
            if (instance.cardSelected1.GetCardID() != instance.cardSelected2.GetCardID())
            {
                instance.StartCoroutine(instance.cardSelected1.CardReset());
                instance.StartCoroutine(instance.cardSelected2.CardReset());

                instance.cardSelected1 = null;
                instance.cardSelected2 = null;

                SoundManager.PlaySound(SoundType.CardMismatch);
                UIManager.ResetCombo();
            }
            else
            {
                instance.cardSelected1.CardMatched();
                instance.cardSelected2.CardMatched();

                instance.SaveCard(instance.cardSelected1);
                instance.SaveCard(instance.cardSelected2);

                instance.cardSelected1 = null;
                instance.cardSelected2 = null;
                SoundManager.PlaySound(SoundType.CardMatch);
                UIManager.UpdateScore();
                UIManager.UpdateMatches();
            }
        }
    }

    public void QuitGame()
    {
        cards.Clear();
        Application.Quit();
    }


    public void SaveCards()
    {
        string saveName = "CardSave";

        for (int i = 0; i < cards.Count; i++)
        {
            PlayerPrefs.SetString(saveName + i, cards[i].GetCardID());
            PlayerPrefs.SetInt(i.ToString(), 0);
        }

    }

    public void SaveCard(CardController _cc)
    {
        int saveName = cards.IndexOf(_cc);

        PlayerPrefs.SetInt(saveName.ToString(), 1);
    }

    public void LoadCards()
    {
        OnGameStart?.Invoke();

        cards.Clear();

        string saveName = "CardSave";
        for (int i = 0; i < PlayerPrefs.GetInt("NumberOfCards"); i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            CardController cardController = newCard.GetComponent<CardController>();
            cardController.SetCardID(PlayerPrefs.GetString(saveName + i));
            cards.Add(cardController);

            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                cardController.CardMatched();
            }
        }

        UIManager.SetTotalMatches(cards.Count / 2);

        int matchedPairs = 0;
        for (int i = 0; i < PlayerPrefs.GetInt("NumberOfCards"); i++)
        {
            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                matchedPairs++;
            }
        }

        UIManager.SetMatches(matchedPairs / 2);

        UIManager.SetScore(PlayerPrefs.GetInt(UIManager.ScoreSaveName), PlayerPrefs.GetInt(UIManager.ComboSaveName));

        UIManager.SetTurns(PlayerPrefs.GetInt(UIManager.TurnsSaveName));

    }

    public static void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("NumberOfCards", 0);

        foreach (CardController card in instance.cards)
        {
            Destroy(card.gameObject);
        }

        instance.cards.Clear();
    }

}
