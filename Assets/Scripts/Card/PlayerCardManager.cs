using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCardManager : MonoBehaviour
{
    private int numCardsOnHand = 0;
    private int maxNumOfCards = 5;
    private bool canDraw;
    private int maxDraw;
    private int curDraw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Card> Deck = new List<Card>();
    public GameObject PlayerCardsArea;
    public List<CardHolder> CardHolder = new List<CardHolder>();
    public List<CombatPlacement> CombatPlacement = new List<CombatPlacement>();
    public TMP_Text NumOfCardText;
    public EventTrigger deckEvent;
    public Image deckImage;
    
   
    public int NumCardsOnhand
    {
        get { return numCardsOnHand; }
        set { numCardsOnHand = value; }
    }
    void Start()
    {
        NumOfCardText.text = Deck.Count + "/20";
        maxDraw = 2;
        canDraw = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        if(numCardsOnHand < maxNumOfCards && canDraw)
        {
            if (Deck.Count > 0)
            {
                //Remove card randomly from deck
                numCardsOnHand++;
                int index = Random.Range(0, Deck.Count);
                Card card = Deck[index];
                Deck.Remove(Deck[index]);
                Instantiate(card, PlayerCardsArea.transform);
                NumOfCardText.text = Deck.Count + "/20";
                curDraw++;
                if (curDraw == maxDraw)
                {
                    deckEvent.enabled = false;
                    deckImage.color =  Color.gray;
                    canDraw = false;

                }
            }

        }
        
    }

    public void ResetDeck()
    {
        if (Deck.Count <= 0) {
            deckEvent.enabled = false;
            deckImage.color = Color.gray;
        }
        else
        {
            deckEvent.enabled = true;
            deckImage.color = new Color(1, 0, 1, 1);
        }
        
        curDraw = 0;
        canDraw = true;
    }

}
