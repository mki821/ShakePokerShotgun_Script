using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backBoard;
    [SerializeField] private GameObject inGameCanvas;
    private float _timer = 0f;
    private bool _invincibility = false;

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= 5f && GameManager.Instance.curHp < 100) {
            _timer = 0f;
            GameManager.Instance.SetHP(++GameManager.Instance.curHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!_invincibility && other.gameObject.layer == 8 && !(gameObject.tag == "op")) {
            print(gameObject.tag);
            StartCoroutine(Invincibility());
            GameManager.Instance.curHp -= 15;
            AudioManager.Instance.PlaySound("Bite");
            PoolManager.Instance.Pop("PlayerHit", transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.5f, -0.1f)));
            GameManager.Instance.SetHP(GameManager.Instance.curHp);
            if (GameManager.Instance.curHp <= 0) {
                inGameCanvas.SetActive(false);
                StartCoroutine(DeathCor());
            }
        }
    }

    private IEnumerator Invincibility() {
        _invincibility = true;

        yield return new WaitForSeconds(0.1f);

        _invincibility = false;
    }

    private IEnumerator DeathCor() {
        backBoard.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Last_GameOver");
    }
}
