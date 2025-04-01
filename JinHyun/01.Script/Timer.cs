using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        _timerText.text = $"{(int)(_timer/60f):D2}:{(int)(_timer%60f):D2}";
    }

    private void OnDestroy() {
        GameManager.Instance.SaveTimer(_timerText.text);
    }
}
