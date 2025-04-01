using UnityEngine;
using TMPro;

public class CreditInfo : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text1;
    [SerializeField] private TextMeshPro _text2;

    private int[,] _cardCounter = new int[14,4];
    private int[] _rankingCounter = new int[14];

    private string _content = "";

    private void Awake() {
        _cardCounter = CheckCard.instance.cardCounter;
        _rankingCounter = CheckCard.instance.rankingCounter;
    }

    private void Start() {
        for(int i = 0; i < 4; ++i) {
            int sum = 0;
            for(int k = 0; k < 14; ++k) {
                sum += _cardCounter[k, i];
            }

            _content += "\n\n\n\n\n";
            _content += $"{sum:D4}번 {_cardCounter[0,i] + _cardCounter[13,i]:D3}번";
            for(int j = 1; j < 13; ++j) {
                _content += $"  {_cardCounter[j, i]:D3}번";
            }
        }
        _text1.text = _content;
        _content = "";

        _content += $"High Card : {_rankingCounter[0]}회\n";
        _content += $"One Pair : {_rankingCounter[1]}회\n";
        _content += $"Two Pair : {_rankingCounter[2]}회\n";
        _content += $"Triple : {_rankingCounter[3]}회\n";
        _content += $"Straight : {_rankingCounter[4]}회\n";
        _content += $"Back Straight : {_rankingCounter[5]}회\n";
        _content += $"Mountain : {_rankingCounter[6]}회\n";
        _content += $"Flush : {_rankingCounter[7]}회\n";
        _content += $"Full House : {_rankingCounter[8]}회\n";
        _content += $"Four Card : {_rankingCounter[9]}회\n";
        _content += $"Straight Flush : {_rankingCounter[10]}회\n";
        _content += $"Back Straight Flush : {_rankingCounter[11]}회\n";
        _content += $"Royal Straight Flush : {_rankingCounter[12]}회\n";
        _content += "\n";
        _content += $"처치한 적: {GameManager.Instance.enemyKill}마리\n";
        _content += $"살아남은 시간: {GameManager.Instance.timer}\n";

        _content += $"\n\n\n{Mathf.Floor(GetRandomCard.instance.shapeWeights[0].shapeWeight * 100) / 100f}%";
        for(int i = 1; i < 4; ++i) {
            _content += $"  {Mathf.Floor(GetRandomCard.instance.shapeWeights[i].shapeWeight * 100) / 100f}%";
        }

        _text2.text = _content;
    }
}
