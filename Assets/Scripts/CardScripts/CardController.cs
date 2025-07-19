using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    string cardID;

    [SerializeField]
    GameObject cardFront;

    [SerializeField]
    GameObject cardBack;

    [SerializeField]
    TMP_Text cardTextObject = null;

    [SerializeField]
    bool isFlipped = false;

    public bool canFlip { get; private set; }


    private void Awake()
    {
        cardTextObject = cardFront.GetComponentInChildren<TMP_Text>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canFlip)
        {
            return;
        }
        FlipCard();
        GameManager.SelectCard(this);
    }


    public void FlipCard()
    {
        if (!canFlip)
        {
            return;
        }

        isFlipped = !isFlipped;
        cardFront.transform.DOLocalRotate(
            new Vector3(0, !isFlipped ? 180 : 0, 0),
            0.5f
        ).SetEase(Ease.OutBack);

        cardBack.transform.DOLocalRotate(
           new Vector3(0, isFlipped ? 180 : 0, 0),
          0.5f
       ).SetEase(Ease.OutBack);
    }

    public void CardSetup()
    {
        cardTextObject.text = cardID;
        canFlip = true;
    }

    public string GetCardID()
    {
        return cardID;
    }

    public void SetCardID(string id)
    {
        cardID = id;
        CardSetup();
        StartCoroutine(ShowCardAndReset());
    }

    IEnumerator ShowCardAndReset()
    {
        FlipCard();
        yield return new WaitForSeconds(1f);
        FlipCard();
    }

    public IEnumerator CardReset()
    {
        yield return new WaitForSeconds(0.25f);
        FlipCard();
    }

    public void CardMatched()
    {
        canFlip = false;
    }

}
