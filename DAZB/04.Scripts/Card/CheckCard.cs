using System.Collections.Generic;
using UnityEngine;

public class CheckCard : MonoBehaviour
{
    public static CheckCard instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
        playerCards = new Card[5];
    }
    public Card[] playerCards;
    public RankingInfo rankingInfo;
    public int[] rankingCounter = new int[14];
    public int[,] cardCounter = new int[14,4];
    public int drawCount = 0;
    public bool isCheat;
    public int cheatRankCount = 0;

    public RankingInfo GetInfo() {
        return rankingInfo;
    }
    

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.P)) {
            isCheat = !isCheat;
            print("cheat " + isCheat);
        }
        if (isCheat) {
            if (Input.GetKeyDown(KeyCode.PageUp)) {
                if (cheatRankCount >= 12) return; 
                cheatRankCount++;
            }
            else if (Input.GetKeyDown(KeyCode.PageDown)) {
                if (cheatRankCount <= 0) return; 
                cheatRankCount--;
            }
        }
    }

    public void DrawCard() {
        if (isCheat) {
            SetCard(cheatRankCount);
        }
        else {
            GetCard();
        }
        rankingInfo = CheckedCard();
        rankingCounter[(int)rankingInfo.ranking]++;
        ShuffleCards();
        foreach (Card iter in playerCards) {
            cardCounter[iter.cardNumber - 1, (int)iter.cardShape]++;
        }
    }

    public int GetCardCount(Card card) {
        print(card.cardNumber - 1);
        return cardCounter[card.cardNumber - 1, (int)card.cardShape];
    }

    public void ResetCounter() {
        rankingCounter = new int[14];
        cardCounter = new int[14,4];
        isCheat = false;
        cheatRankCount = 0;
    }

    private void GetCard() {
        for (int i = 0; i < 5; ++i) {
            Card newCard = GetRandomCard.instance.GetRandom();
            foreach (Card iter in playerCards) {
                if (iter == null) continue;
                if ((newCard.cardNumber == iter.cardNumber || 
                 (newCard.cardNumber == 1 && iter.cardNumber == 14) || 
                 (newCard.cardNumber == 14 && iter.cardNumber == 1)) && 
                newCard.cardShape == iter.cardShape) {
                    print("" + newCard.cardNumber + newCard.cardShape + " " + iter.cardNumber + iter.cardShape);
                    GetCard();
                    return;
                }
            }
            playerCards[i] = newCard;
        }
        drawCount++;
    }

    private void SetCard(int val) {
        switch (val) {
            case 0: {
                playerCards[0] = new Card(CardShape.SPADE, 14);
                playerCards[1] = new Card(CardShape.SPADE, 3);
                playerCards[2] = new Card(CardShape.SPADE, 5);
                playerCards[3] = new Card(CardShape.CLUB, 8);
                playerCards[4] = new Card(CardShape.HEART, 2);
                break;
            }
            case 1: {
                playerCards[0] = new Card(CardShape.SPADE, 14);
                playerCards[1] = new Card(CardShape.SPADE, 14);
                playerCards[2] = new Card(CardShape.SPADE, 5);
                playerCards[3] = new Card(CardShape.CLUB, 8);
                playerCards[4] = new Card(CardShape.HEART, 2);
                break;
            }
            case 2: {
                playerCards[0] = new Card(CardShape.SPADE, 6);
                playerCards[1] = new Card(CardShape.SPADE, 6);
                playerCards[2] = new Card(CardShape.SPADE, 9);
                playerCards[3] = new Card(CardShape.CLUB, 8);
                playerCards[4] = new Card(CardShape.HEART, 9);
                break;
            }
            case 3: {
                playerCards[0] = new Card(CardShape.SPADE, 1);
                playerCards[1] = new Card(CardShape.SPADE, 2);
                playerCards[2] = new Card(CardShape.SPADE, 6);
                playerCards[3] = new Card(CardShape.CLUB, 1);
                playerCards[4] = new Card(CardShape.HEART, 1);
                break;
            }
            case 4: {
                playerCards[0] = new Card(CardShape.SPADE, 2);
                playerCards[1] = new Card(CardShape.SPADE, 3);
                playerCards[2] = new Card(CardShape.SPADE, 4);
                playerCards[3] = new Card(CardShape.CLUB, 5);
                playerCards[4] = new Card(CardShape.HEART, 6);
                break;
            }
            case 5: {
                playerCards[0] = new Card(CardShape.SPADE, 1);
                playerCards[1] = new Card(CardShape.SPADE, 2);
                playerCards[2] = new Card(CardShape.SPADE, 3);
                playerCards[3] = new Card(CardShape.CLUB, 4);
                playerCards[4] = new Card(CardShape.HEART, 5);
                break;
            }
            case 6: {
                playerCards[0] = new Card(CardShape.SPADE, 10);
                playerCards[1] = new Card(CardShape.SPADE, 11);
                playerCards[2] = new Card(CardShape.SPADE, 12);
                playerCards[3] = new Card(CardShape.SPADE, 13);
                playerCards[4] = new Card(CardShape.HEART, 14);
                break;
            }
            case 7: {
                playerCards[0] = new Card(CardShape.SPADE, 9);
                playerCards[1] = new Card(CardShape.SPADE, 1);
                playerCards[2] = new Card(CardShape.SPADE, 4);
                playerCards[3] = new Card(CardShape.SPADE, 5);
                playerCards[4] = new Card(CardShape.SPADE, 2);
                break;
            }
            case 8: {
                playerCards[0] = new Card(CardShape.HEART, 3);
                playerCards[1] = new Card(CardShape.DIAMOND, 3);
                playerCards[2] = new Card(CardShape.SPADE, 3);
                playerCards[3] = new Card(CardShape.SPADE, 2);
                playerCards[4] = new Card(CardShape.HEART, 2);
                break;
            }
            case 9: {
                playerCards[0] = new Card(CardShape.SPADE, 14);
                playerCards[1] = new Card(CardShape.CLUB, 14);
                playerCards[2] = new Card(CardShape.DIAMOND, 14);
                playerCards[3] = new Card(CardShape.CLUB, 2);
                playerCards[4] = new Card(CardShape.HEART, 14);
                break;
            }
            case 10: {
                playerCards[0] = new Card(CardShape.SPADE, 2);
                playerCards[1] = new Card(CardShape.SPADE, 3);
                playerCards[2] = new Card(CardShape.SPADE, 4);
                playerCards[3] = new Card(CardShape.SPADE, 5);
                playerCards[4] = new Card(CardShape.SPADE, 6);
                break;
            }
            case 11: {
                playerCards[0] = new Card(CardShape.SPADE, 1);
                playerCards[1] = new Card(CardShape.SPADE, 2);
                playerCards[2] = new Card(CardShape.SPADE, 3);
                playerCards[3] = new Card(CardShape.SPADE, 4);
                playerCards[4] = new Card(CardShape.SPADE, 5);
                break;
            }
            case 12: {
                playerCards[0] = new Card(CardShape.SPADE, 10);
                playerCards[1] = new Card(CardShape.SPADE, 11);
                playerCards[2] = new Card(CardShape.SPADE, 12);
                playerCards[3] = new Card(CardShape.SPADE, 13);
                playerCards[4] = new Card(CardShape.SPADE, 14);
                break;
            }
        }
    }

    public void UpRanking() {
        if (cheatRankCount >= 12) return; 
                cheatRankCount++;
    }

    public void DownRanking() {
        if (cheatRankCount <= 0) return; 
                cheatRankCount--;
    }

    private void ShuffleCards() {
        for (int i = 0; i < playerCards.Length; i++) {
            int randIdx = Random.Range(i, playerCards.Length);
            (playerCards[i], playerCards[randIdx]) = (playerCards[randIdx], playerCards[i]);  
        }
    }

    private RankingInfo CheckedCard() {
        if (HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item2.cardShape + " Royal Straight Flush");        
            return new RankingInfo(Ranking.ROYALSTRAIGHTFLUSH, HandRankings.instance.RoyalStraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.BackStraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.BackStraightFlushCheck(playerCards).Item2.cardShape + " Back Straight Flush");
            return new RankingInfo(Ranking.BACKSTRAIGHTFLUSH, HandRankings.instance.BackStraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.StraightFlushCheck(playerCards).Item1) {
            print(HandRankings.instance.StraightFlushCheck(playerCards).Item2.cardShape + " Straight Flush");
            return new RankingInfo(Ranking.STRAIGHTFLUSH, HandRankings.instance.StraightFlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.FourCardCheck(playerCards).Item1) {
            print(HandRankings.instance.FourCardCheck(playerCards).Item2.cardNumber + " Four card");
            return new RankingInfo(Ranking.FOURCARD, HandRankings.instance.FourCardCheck(playerCards).Item2);
        }
        if (HandRankings.instance.FullHouseCheck(playerCards)) {
            print(" FullHouse");
            return new RankingInfo(Ranking.FULLHOUSE);
        }
        if (HandRankings.instance.FlushCheck(playerCards).Item1) {
            print(HandRankings.instance.FlushCheck(playerCards).Item2.cardShape + " Flush");
            return new RankingInfo(Ranking.FLUSH, HandRankings.instance.FlushCheck(playerCards).Item2);
        }
        if (HandRankings.instance.MountainCheck(playerCards)) {
            print(" Mountine");
            return new RankingInfo(Ranking.MOUNTAIN);
        }
        if (HandRankings.instance.BackStraightCheck(playerCards)) {
            print(" Back straight");
            return new RankingInfo(Ranking.BACKSTRAIGHT);
        }
        if (HandRankings.instance.StraightCheck(playerCards)) {
            print(" Straight");
            return new RankingInfo(Ranking.STRAIGHT);
        }
        if (HandRankings.instance.TripleCheck(playerCards).Item1) {
            print(HandRankings.instance.TripleCheck(playerCards).Item2.cardNumber + " Triple");
            return new RankingInfo(Ranking.TRIPLE, HandRankings.instance.TripleCheck(playerCards).Item2);
        }
        if (HandRankings.instance.TwoPairCheck(playerCards).Item1) {
            print(/* HandRankings.instance.TwoPairCheck(playerCards).Item2[0].cardNumber +  */" Two pair");
            return new RankingInfo(Ranking.TWOPAIR, HandRankings.instance.TwoPairCheck(playerCards).Item2);
        }
        if (HandRankings.instance.OnePairCheck(playerCards).Item1) {
            print(HandRankings.instance.OnePairCheck(playerCards).Item2.cardNumber + " One pair");
            return new RankingInfo(Ranking.ONEPAIR, HandRankings.instance.OnePairCheck(playerCards).Item2);
        }
        print(HandRankings.instance.HighCardCheck(playerCards).Item2.cardNumber + " High Card");
        return new RankingInfo(Ranking.HIGHCARD, HandRankings.instance.HighCardCheck(playerCards).Item2);
    }
}
