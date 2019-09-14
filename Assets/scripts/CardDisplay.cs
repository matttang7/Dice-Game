using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour {

    // Use this for initialization
    public Card card;
    public Text attack;
    public Text defense;
    public Text level;
    public Image cardArt;
	void Start () {
        setCard(card.artwork, card.attack.ToString(), card.defense.ToString(), card.level.ToString());
	}

    void setCard(Sprite artwork, string att, string def, string lvl)
    {
        cardArt.sprite = artwork;
        attack.text = att;
        defense.text = def;
        level.text = lvl;
    }

    void Update()
    {
        setCard(card.artwork, card.attack.ToString(), card.defense.ToString(), card.level.ToString());
    }
}
