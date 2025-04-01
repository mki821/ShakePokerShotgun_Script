using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRankings : MonoBehaviour
{
    public static HandRankings instance;

    private void Awake() {
        instance = this;
    }

    // High card
    public (bool, Card) HighCardCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        return (true, sortedCard[sortedCard.Length - 1]);
    }

    // One Pair
    public (bool, Card) OnePairCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            if (cards[i].cardNumber == 1 || cards[i].cardNumber == 14) {
                for (int j = 0; j < cards.Length; ++j) {
                    if (i != j && (cards[i].cardNumber == cards[j].cardNumber || 
                                   (cards[i].cardNumber == 1 && cards[j].cardNumber == 14) || 
                                   (cards[i].cardNumber == 14 && cards[j].cardNumber == 1))) {
                        ++same;
                        if (same == 2) {
                            return (true, cards[i]);
                        }
                    }
                }
            }
            else {
                for (int j = 0; j < cards.Length; ++j) {
                    if (i != j && cards[i].cardNumber == cards[j].cardNumber) {
                        ++same;
                        if (same == 2) {
                            return (true, cards[i]);
                        }
                    }
                }
            }
        }
        return (false, null);
    }


    // Two Pair
    public (bool, Card[]) TwoPairCheck(Card[] cards) {
        Card[] pairCard = new Card[2];
        int pairCount = 0;
        
        for (int i = 0; i < cards.Length; ++i) {
            for (int j = i + 1; j < cards.Length; ++j) {
                if (cards[i].cardNumber == cards[j].cardNumber || 
                   (cards[i].cardNumber == 1 && cards[j].cardNumber == 14) || 
                   (cards[i].cardNumber == 14 && cards[j].cardNumber == 1)) {
                    pairCount++;
                    pairCard[pairCount - 1] = cards[i];
                    
                    if (pairCount == 2) {
                        return (true, pairCard);
                    }
                }
            }
        }
        return (false, null);
    }

    // Three of a kind
    public (bool, Card) TripleCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            for (int j = 0; j < cards.Length; ++j) {
                if (i != j && (cards[i].cardNumber == cards[j].cardNumber || 
                           (cards[i].cardNumber == 1 && cards[j].cardNumber == 14) ||
                           (cards[i].cardNumber == 14 && cards[j].cardNumber == 1))) {
                    ++same;
                    if (same == 3) {
                        return (true, cards[i]);
                    }
                }
            }
        }
        return (false, null);
    }

    // Straight
    public bool StraightCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (sortedCard[i].cardNumber - sortedCard[i - 1].cardNumber != 1) {
                return false;
            }
        }
        return true;
    }

    // BackStraight
    public bool BackStraightCheck(Card[] cards) {
        Card[] sortedCard = CardSort(cards);
        if (!StraightCheck(sortedCard)) return  false;
        if (sortedCard[0].cardNumber != 1) return false;
        return true;
    }

    // Mountain
    public bool MountainCheck(Card[] cards) {
    Card[] sortedCard = CardSort(cards);
        // 10부터 시작하는 경우 체크
        if (sortedCard[0].cardNumber == 10) {
            for (int i = 1; i < sortedCard.Length; ++i) {
                if (sortedCard[i].cardNumber != sortedCard[i - 1].cardNumber + 1) {
                    return false;
                }
            }
            return true;
        }

        // 1부터 시작하는 경우 체크
        if (sortedCard[0].cardNumber == 1) {
            // 카드가 1, 2, 3, 4, 5인 경우 체크
            if (sortedCard[1].cardNumber == 10 &&
                sortedCard[2].cardNumber == 11 &&
                sortedCard[3].cardNumber == 12 &&
                sortedCard[4].cardNumber == 13) {
                return true;
            }
        }

        return false;
    }


    // Flush
    public (bool, Card) FlushCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) continue;
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return (false, null);
            }
        }
        return (true, cards[0]);
    }

    // Full house
    public bool FullHouseCheck(Card[] cards) {
        Dictionary<int, int> cardCount = new Dictionary<int, int>();

        foreach (Card iter in cards) {
            if (cardCount.ContainsKey(iter.cardNumber)) {
                cardCount[iter.cardNumber]++;
            } else {
                cardCount.Add(iter.cardNumber, 1);
            }
        }

        bool hasTriple = false;
        bool hasPair = false;

        foreach (int count in cardCount.Values) {
            if (count == 3) {
                hasTriple = true;
            } else if (count == 2) {
                hasPair = true;
            }
        }

        return hasTriple && hasPair;
    }

    // four of a kind
    public (bool, Card) FourCardCheck(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int same = 1;
            for (int j = 0; j < cards.Length; ++j) {
                if (i != j && (cards[i].cardNumber == cards[j].cardNumber || 
                           (cards[i].cardNumber == 1 && cards[j].cardNumber == 14) ||
                           (cards[i].cardNumber == 14 && cards[j].cardNumber == 1))) {
                    ++same;
                    if (same == 4) {
                        return (true, cards[i]);
                    }
                }
            }
        }
        return (false, null);
    }

    // Straight Flush
    public (bool, Card) StraightFlushCheck(Card[] cards) {
        if (!StraightCheck(cards)) return (false, null);
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return (false, null);   
            }
        }
        return (true, cards[0]);
    }

    // back straight flush
    public (bool, Card) BackStraightFlushCheck(Card[] cards) {
        if (!BackStraightCheck(cards)) return (false, null);
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return (false, null);   
            }
        }
        return (true, cards[0]);
    }

    // Royal Straight Flush
    public (bool, Card) RoyalStraightFlushCheck(Card[] cards) {
        print(MountainCheck(cards));
        if (!MountainCheck(cards)) return (false, null);
        for (int i = 0; i < cards.Length; ++i) {
            if (i == 0) {
                continue;
            }
            else if (cards[i].cardShape != cards[i - 1].cardShape) {
                return (false, null);   
            }
        }
        return (true, cards[0]);
    }

/// <summary>
/// 카드 배열을 정렬
/// </summary>
/// <param name="cards">카드의 배열</param>
    public Card[] CardSort(Card[] cards) {
        for (int i = 0; i < cards.Length; ++i) {
            int min = i;
            for (int j = i + 1; j < cards.Length; ++j) {
                if (cards[min].cardNumber > cards[j].cardNumber) {
                    min = j;
                }
            }
            (cards[i], cards[min]) = (cards[min], cards[i]);
        }
        return cards;
    }

    public Card[] ShuffleArray(Card[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int randIndex = UnityEngine.Random.Range(0, i + 1);

            // 요소를 교환
            Card temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
        return array;
    }
}
