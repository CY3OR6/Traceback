using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardController : MonoBehaviour, IPointerClickHandler
{


    [SerializeField]
    GameObject cardFront;

    [SerializeField]
    GameObject cardBack;

    bool isFlipped = false;



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
        FlipCard();
    }


    public void FlipCard()
    {
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

}
