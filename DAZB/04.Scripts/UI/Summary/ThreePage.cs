using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreePage : MonoBehaviour
{
    public TMP_Text[] texts;
    private TextSetter textSetter;

    private void Awake() {
        textSetter = GetComponentInChildren<TextSetter>();
    }

    private void OnEnable() {
        textSetter.SetPercentText();
        texts[0].text = "처치한 적의 수 : " + GameManager.Instance.enemyKill;
        texts[1].text = "생존한 시간 : " + GameManager.Instance.timer; 
    }
}
