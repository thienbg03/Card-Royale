using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    private int numCardsOnHand = 0;
    private int maxNumOfCards = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Card> Deck = new List<Card>();
    public GameObject PlayerCardsArea;

    public int NumCardsOnhand
    {
        get { return numCardsOnHand; }
        set { numCardsOnHand = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        if(numCardsOnHand < maxNumOfCards)
        {
            if (Deck.Count > 0)
            {
                //Remove card randomly from deck
                numCardsOnHand++;
                int index = Random.Range(0, Deck.Count);
                Card card = Deck[index];
                Deck.Remove(Deck[index]);
                Instantiate(card, PlayerCardsArea.transform);
            }

        }
        
    }

}
