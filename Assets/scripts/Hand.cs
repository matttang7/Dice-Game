using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public Deck deck;
    public List<Card> cards;
    void Start()
    {
        if(deck.otherCards.Count < 8)
        {
            deck.shuffleHand();
        }
        deck.drawHand();
    }
    void Update()
    {
        cards = deck.hand;
    }
}
