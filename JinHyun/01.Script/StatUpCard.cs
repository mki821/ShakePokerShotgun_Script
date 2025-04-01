using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StatUpCard : MonoBehaviour
{
    StatUpCanvas statUpParent;
    [SerializeField] private GameObject[] Friends;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private char[] shapes = new char[] { '♠', '♦', '♥', '♣' };

    Image jokerImage;
    Vector2 originPos;
    Vector2 originScale;
    int randShape;
    float randPercent;

    RectTransform rect;
    bool selected = false;
    private void Awake()
    {
        jokerImage = GetComponent<Image>();
        description = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originPos = transform.position;
        originScale = transform.localScale;
        rect = GetComponent<RectTransform>();
        statUpParent = transform.parent.GetComponent<StatUpCanvas>();
    }
    private void OnEnable()
    {
        if (jokerImage.material == statUpParent.cardShinyMat)
        {
            print("원상태");
            jokerImage.material = null;
        }
        selected = false;
        randShape = Random.Range(0, shapes.Length);
        randPercent = Random.Range(1, 4) + Random.Range(1, 10) * 0.1f;
        description.text = $"{shapes[randShape]}카드가 나올 확률이\n" +
                           $"{randPercent}% 증가합니다.";
        transform.position = originPos;
        transform.localScale = originScale;
        transform.rotation = Quaternion.identity;
        transform.DORotate(new Vector2(0, 360), 0.6f, RotateMode.WorldAxisAdd)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => EnableText())
            .SetUpdate(true);
    }
    void EnableText()
    {
        if (transform.name == "Option3")
        {
            transform.parent.GetComponent<StatUpCanvas>().text.SetActive(true);
            transform.parent.GetComponent<StatUpCanvas>().shieldPanel.SetActive(false);
            Time.timeScale = 0;
        }
    }

    void DisableFriends()
    {
        foreach (GameObject item in Friends)
        {
            item.SetActive(false);
        }
    }
    public void SelectCard()
    {
        //Disable GameObject Except Selected Card
        DisableFriends();
        transform.parent.GetComponent<StatUpCanvas>().shieldPanel.SetActive(true);
        statUpParent.DisableText();
        selected = true;
        Exit();

        //Add Weight to Selected Card's Increase Stat Info
        GetRandomCard.instance.SetWeight(randShape, randPercent);
        //Selected Card's Position to Move Zero, Add Shiny Effect, Decrease Size to 0
        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.Join(transform.DOScale(new Vector2(8, 8), 0.4f));
        seq.Append(transform.DOMove(Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)), 0.4f).SetEase(Ease.OutQuad));
        seq.AppendInterval(0.4f);

        seq.Append(transform.DOScale(new Vector2(0, 0), 0.4f).SetEase(Ease.OutQuad).OnComplete(
            () =>
            {
                statUpParent.DisableAll();
                Time.timeScale = 1;
                AudioManager.Instance.PlayBGM("InGame");
                GameManager.Instance.player.CheckRemainLevelUP();
            }));
    }

    public void Hover() { if (!selected) rect.DOScale(new Vector3(originScale.x * 1.1f, originScale.y * 1.1f), 0.1f).SetEase(Ease.Linear).SetUpdate(true); }

    public void Exit() { if (!selected) rect.DOScale(originScale, 0.15f).SetEase(Ease.Linear).SetUpdate(true); }

}
