using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject[] ui;
    public Animator playerAnim;
    public RuntimeAnimatorController changeController;
    public Button[] pageChangeButtons; // 1: left 2: right
    public Button[] cards;

    private void Start() {
        PlayerDeath();
    }

    public void PlayerDeath() {
        playerAnim.runtimeAnimatorController = changeController;
        StartCoroutine(DeathCor());
    }

    public void NextScene(string name) {
        CheckCard.instance.ResetCounter();
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(name);
    }

    private IEnumerator DeathCor() {
/*         playerAnim.speed = 0;
        gameObject.GetComponent<Image>().DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        playerAnim.speed = 1;
        playerAnim.SetTrigger("Death");
        yield return new WaitForSeconds(0.7f);
        ui[0].GetComponent<TMP_Text>().DOColor(Color.white, 0.2f);
        yield return new WaitForSeconds(0.4f);
        ui[1].GetComponent<Image>().DOColor(Color.white, 0.2f);
        yield return new WaitForSeconds(0.4f);
        ui[2].GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        yield return null; */
        yield return new WaitForSeconds(0.5f);
        ui[0].GetComponent<TMP_Text>().DOColor(Color.white, 1.4f);
        ui[1].GetComponent<RectTransform>().DOScale(new Vector3(1, 0.01f, 0), 0.8f);
        yield return new WaitForSeconds(1f);
        ui[1].GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 0), 0.3f);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < cards.Length; ++i) {
            cards[i].GetComponent<Image>().DOFade(1, 0.2f);
        }
        pageChangeButtons[1].GetComponent<Image>().DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.4f);
        ui[2].GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }
}
