using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    [SerializeField] private bool _boss = false;
    [SerializeField] private float _magneticDistance = 5f;
    [SerializeField] private float _speed = 10f;

    private bool _findTarget = false;

    private Transform _playerTrm;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void OnEnable() {
        _findTarget = false;
        _spriteRenderer.color = _boss ? new Color(1, Random.Range(0f, 1f), 0) : new Color(0, 1, Random.Range(0f, 1f));
    }

    private void Update() {
        float distance = Vector2.Distance(transform.position, _playerTrm.position);
        if(_findTarget) {
            Vector3 direction = _playerTrm.position - transform.position;
            if(distance < 1.5f) transform.position += direction.normalized * Time.deltaTime * (1/1.5f/_magneticDistance) * _speed;
            else if(distance > 5f) transform.position += direction.normalized * Time.deltaTime * (1/5f/_magneticDistance) * _speed;
            else transform.position += direction.normalized * Time.deltaTime * (1/distance/_magneticDistance) * _speed;
        }
        else {
            if(distance <= _magneticDistance) _findTarget = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("op"))
        {
            int ran = Random.Range(0, 2);
            AudioManager.Instance.PlaySound("GetExp" + ran);
            PushEXP();
        }
    }
    public void PushEXP()
    {
        GameManager.Instance.player.ExpUP(_boss ? 300 : 30);
        PoolManager.Instance.Push(_boss ? "BossExp" : "Exp", gameObject);
    }
}
