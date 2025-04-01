using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Ranking
{
    HIGHCARD,
    ONEPAIR,
    TWOPAIR,
    TRIPLE,
    STRAIGHT,
    BACKSTRAIGHT,
    MOUNTAIN,
    FLUSH,
    FULLHOUSE,
    FOURCARD,
    STRAIGHTFLUSH,
    BACKSTRAIGHTFLUSH,
    ROYALSTRAIGHTFLUSH,
    NONE
}

[System.Serializable]
public class RankingInfo {
    public RankingInfo(Ranking ranking, Card cardData) {
        this.ranking = ranking;
        this.cardData1 = cardData;
        this.cardData2 = null;
    }

    public RankingInfo(Ranking ranking, Card[] cardData) {
        this.ranking = ranking;
        this.cardData1 = cardData[0];
        this.cardData2 = cardData[1];
    }

    public RankingInfo(Ranking ranking) {
        this.ranking = ranking;
    }
    public Ranking ranking;
    public Card cardData1;
    public Card cardData2;
}
