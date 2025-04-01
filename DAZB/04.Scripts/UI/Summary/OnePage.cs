using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnePage : MonoBehaviour
{
    public Button[] cards;
    public RectTransform panel;
    public Button backButton;
    public Button[] pageChangeButtons; // 1: left 2: right
    public GameObject[] GeneratedCard;
    public Sprite[] spade;
    public Sprite[] diamond;
    public Sprite[] heart;
    public Sprite[] clover;
    private int idx;

    private void Awake() {
        GeneratedCard = new GameObject[spade.Length];
    }

    public void ClickClickClickCard(int idx) {
        this.idx = idx;
        StartCoroutine(ClickCardCor(idx));
    }

    public void BackButton() {
        StartCoroutine(BackCor());
    }

    private IEnumerator BackCor() {
        backButton.interactable = false;
        backButton.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject iter in GeneratedCard.Reverse()) {
            Destroy(iter);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        cards[idx].GetComponentInChildren<TMP_Text>().text = "";
        yield return new WaitForSeconds(0.1f);
        cards[idx].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
        cards[idx].GetComponent<RectTransform>().DOScale(new Vector3(1, 1 / 1.5f), 1f);
        panel.DOScale(new Vector3(1f, 1f), 1f);
        cards[idx].GetComponent<RectTransform>().DOScale(new Vector3(1, 1), 1f);
        yield return new WaitForSeconds(1f);
        switch (idx) {
            case 0: {
                cards[idx].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-137.5f, 167.5f), 0.5f);
                break;
            }
            case 1: {
                cards[idx].GetComponent<RectTransform>().DOAnchorPos(new Vector2(137.5f, 167.5f), 0.5f);
                break;
            }
            case 2: {
                cards[idx].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-137.5f, -167.5f), 0.5f);
                break;
            }
            case 3: {
                cards[idx].GetComponent<RectTransform>().DOAnchorPos(new Vector2(137.5f, -167.5f), 0.5f);
                break;
            }
        }
        foreach (var iter in cards) {
            iter.GetComponent<Image>().DOFade(1, 0.2f);
            iter.interactable = true;
        }
        pageChangeButtons[1].GetComponent<Image>().DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        foreach (var iter in cards) {
            iter.interactable = true;
        }
        pageChangeButtons[1].interactable = true;
    }

    private IEnumerator ClickCardCor(int idx) {
        for (int i = 0; i < cards.Length; ++i) {
            if (i == idx) {
                cards[i].interactable = false;
                continue;
            }
            cards[i].GetComponent<Image>().DOFade(0, 0.2f).OnComplete(() => {
                cards[i].gameObject.SetActive(false); 
            });
            cards[i].interactable = false;
        }
        pageChangeButtons[1].GetComponent<Image>().DOFade(0, 0.2f);
        pageChangeButtons[1].interactable = false;
        yield return new WaitForSeconds(0.3f);
        cards[idx].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
        yield return new WaitForSeconds(0.2f);
        panel.DOScale(new Vector3(1f, 1.5f), 1f);
        cards[idx].GetComponent<RectTransform>().DOScale(new Vector3(1, 1 / 1.5f), 1f);
        yield return new WaitForSeconds(1f);
        cards[idx].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-240, 220), 0.5f); // x 160 y 220
        cards[idx].GetComponent<RectTransform>().DOScale(new Vector3(0.6f, 0.6f / 1.5f), 0.5f);
        yield return new WaitForSeconds(0.5f);
        cards[idx].GetComponentInChildren<TMP_Text>().text = (CheckCard.instance.GetCardCount(new Card((CardShape)idx, 1)) + CheckCard.instance.GetCardCount(new Card((CardShape)idx, 14))).ToString();
        for (int i = 1; i < spade.Length; ++i) {
            yield return new WaitForSeconds(0.05f);
            int j = i / 4;
    
            GeneratedCard[i - 1] = Instantiate(cards[idx].gameObject, Vector2.zero, Quaternion.identity);
            GeneratedCard[i - 1].gameObject.transform.SetParent(transform);
            GeneratedCard[i - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-240 + 160 * (i % 4), 220 - 140 * j);
            GeneratedCard[i - 1].GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f / 1.5f);
            if (i == spade.Length - 1) {
                GeneratedCard[i - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 220 - 140 * j);
            }
            switch (idx) {
                case 0: {
                    GeneratedCard[i - 1].GetComponent<Image>().sprite = spade[i];
                    break;
                }
                case 1: {
                    GeneratedCard[i - 1].GetComponent<Image>().sprite = diamond[i];
                    break;
                }
                case 2: {
                    GeneratedCard[i - 1].GetComponent<Image>().sprite = heart[i];
                    break;
                }
                case 3: {
                    GeneratedCard[i - 1].GetComponent<Image>().sprite = clover[i];
                    break;
                }
            }
            //CheckCard.instance.GetCardCount(new Card((CardShape)idx, i - 1));
            GeneratedCard[i - 1].GetComponentInChildren<TMP_Text>().gameObject.SetActive(true);
            GeneratedCard[i - 1].GetComponentInChildren<TMP_Text>().text = "" + CheckCard.instance.GetCardCount(new Card((CardShape)idx, i + 1));
        }
        backButton.interactable = true;
        backButton.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }
    
}
