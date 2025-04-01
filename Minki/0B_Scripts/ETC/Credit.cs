using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private float _speed = 0.5f;

    private bool _first = true;

    private void Update() {
        if(transform.position.y >= 57f) {
            if(_first) {
                _first = false;
                ShowButton();
            }
            return;
        }

        transform.position += Vector3.up * Time.deltaTime * _speed;

        if(Input.GetMouseButton(0)) _speed = 2f;
        else if(Input.GetMouseButton(1)) _speed = 0f;
        else _speed = 1; 
    }

    private void ShowButton() {
        _button.SetActive(true);
    }

    public void GotoMain() {
        Destroy(GameManager.Instance.gameObject);
        CheckCard.instance.ResetCounter();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
