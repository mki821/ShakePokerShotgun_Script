using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardShape {
    SPADE,
    DIAMOND, // red
    HEART, // red
    CLUB
}

[System.Serializable]
public class Card
{
    public Card(CardShape cardShape, int cardNumber) {
        this.cardShape = cardShape;
        this.cardNumber = cardNumber;
    }

    public CardShape cardShape;
    public int cardNumber;
}
