using System.Collections;
using UnityEngine;

public class BossC : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _power = 10f;

    private Transform _playerTrm;
    private Animator _animator;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();
        _animator = GetComponent<Animator>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Start() {
        StartCoroutine(SpawnBoom());
    }

    private void Update() {
        if(_controller.freezeFlip) return;

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
    }

    private IEnumerator SpawnBoom() {
        while(true) {
            yield return new WaitUntil(() => Vector2.Distance(transform.position, _playerTrm.position) < _radius);

            _controller.moveable = false;
            _animator.SetTrigger("attack");
            _controller.freezeFlip = true;

            AudioManager.Instance.PlaySound("MissEvade");
            GameObject obj = PoolManager.Instance.Pop("BoomC", transform.position);
            Vector2 direction = _playerTrm.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * _power, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.7f);

            _controller.moveable = true;
            _controller.freezeFlip = false;

            yield return new WaitForSeconds(0.8f);
        }
    }
}
