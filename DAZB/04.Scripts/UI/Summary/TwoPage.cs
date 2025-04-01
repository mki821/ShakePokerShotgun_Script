using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwoPage : MonoBehaviour
{
    public TMP_Text[] texts;

    private void OnEnable() {
        texts[0].text = "High card: " + CheckCard.instance.rankingCounter[0];
        texts[1].text = "One pair: " + CheckCard.instance.rankingCounter[1];
        texts[2].text = "Two pair: " + CheckCard.instance.rankingCounter[2];
        texts[3].text = "Triple: " + CheckCard.instance.rankingCounter[3];
        texts[4].text = "Straight: " + CheckCard.instance.rankingCounter[4];
        texts[5].text = "Back straight: " + CheckCard.instance.rankingCounter[5];
        texts[6].text = "Mountain: " + CheckCard.instance.rankingCounter[6];
        texts[7].text = "Flush: " + CheckCard.instance.rankingCounter[7];
        texts[8].text = "Full house: " + CheckCard.instance.rankingCounter[8];
        texts[9].text = "Four card: " + CheckCard.instance.rankingCounter[9];
        texts[10].text = "Straight flush: " + CheckCard.instance.rankingCounter[10];
        texts[11].text = "Back straight flush: " + CheckCard.instance.rankingCounter[11];
        texts[12].text = "Royal straight flush: " + CheckCard.instance.rankingCounter[12];
    }
}
