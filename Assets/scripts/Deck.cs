using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newDeck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public List<Card> hand;
    public List<Card> otherCards;
    // Use this for initialization
    void Start()
    {

    }
    public void drawHand()
    {
        for (int i = 0; i < 8; i++)
        {
            int whichCard = 0;
            whichCard = Random.Range(0, otherCards.Count-1);
            hand.Add(otherCards[whichCard]);
            otherCards.RemoveAt(whichCard);
        }
    }
    public void shuffleHand()
    {
        while(hand.Count > 0)
        {
            Card toBeRemoved = hand[0];
            hand.RemoveAt(0);
            otherCards.Add(toBeRemoved);
        }
    }
}
