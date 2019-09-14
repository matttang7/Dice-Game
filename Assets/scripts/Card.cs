using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newCard", menuName = "Card")]
public class Card : ScriptableObject {

    public string cardName;
    public string description;
    public Sprite artwork;
    public int level;
    public int attack;
    public int defense;

}
