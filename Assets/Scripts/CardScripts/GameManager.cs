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

        List<string> _tempCardId = cardIDs;

        for (int i = cardIDs.Count - 1; i >= 0; i--)
        {
            _tempCardId.Add(cardIDs[i]);
        }

        int ri = Random.Range(0, _tempCardId.Count);


        while (_tempCardId.Count > 0)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            newCard.GetComponent<CardController>().SetCardID(_tempCardId[ri]);
            cards.Add(newCard.GetComponent<CardController>());
            _tempCardId.RemoveAt(ri);
            ri = Random.Range(0, _tempCardId.Count);
        }
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

                instance.cardSelected1 = null;
                instance.cardSelected2 = null;
                SoundManager.PlaySound(SoundType.CardMatch);
                UIManager.UpdateScore();
                UIManager.UpdateMatches();
            }
        }

        foreach (CardController item in instance.cards)
        {
            if (item.canFlip)
            {
                return;
            }
        }

        Debug.Log("All cards matched!");
    }

    public void QuitGame()
    {
        cards.Clear();
        Application.Quit();
    }


}
