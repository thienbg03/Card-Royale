using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public List<Card> Deck = new List<Card>();
    public GameObject PlayerCardsArea;
    private int numCardsOnHand = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard()
    {
        if(numCardsOnHand != 7)
        {
            if (Deck.Count > 0)
            {
                //Remove card randomly from deck
                numCardsOnHand++;
                int index = Random.Range(0, Deck.Count);
                Card card = Deck[index];
                Instantiate(card, PlayerCardsArea.transform);
            }

        }
        
    }

    public void SetNumCardsOnHand(int num)
    {
        numCardsOnHand = num;
    }
}
