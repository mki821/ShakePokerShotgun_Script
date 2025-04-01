using System.Collections;
using UnityEngine;
using TMPro;

public class ShowRanking : MonoBehaviour
{
    private TextMeshPro _text;

    private void Awake() {
        _text = GetComponent<TextMeshPro>();
    }

    public void Show(string text, Color color) {
        _text.text = text;
        _text.color = color;
        
        StartCoroutine(ShowRoutine(color));
    }

    private IEnumerator ShowRoutine(Color color) {
        float timer = 0f;

        while(timer < 0.05f) {
            timer += Time.deltaTime;
            _text.fontSize = Mathf.Lerp(2f, 5f, timer * 20);
            yield return null;
        }
        timer = 0f;
        while(timer < 0.05f) {
            timer += Time.deltaTime;
            _text.fontSize = Mathf.Lerp(5f, 2f, timer * 20);
            yield return null;
        }
        timer = 0f;
        while(timer < 1f) {
            timer += Time.deltaTime;
            _text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, timer));
            yield return null;
        }
        Destroy(gameObject);
    }
}
