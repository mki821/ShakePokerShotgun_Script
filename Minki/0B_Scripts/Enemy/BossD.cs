using System.Collections;
using UnityEngine;

public class BossD : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;

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

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    private IEnumerator SpawnBoom() {
        while(true) {
            yield return new WaitUntil(() => Vector2.Distance(transform.position, _playerTrm.position) < _radius);

            _controller.moveable = false;
            _animator.SetTrigger("attack");
            _controller.freezeFlip = true;

            PoolManager.Instance.Pop("Boom", _playerTrm.position);
            AudioManager.Instance.PlaySound("EnemyDeath");

            yield return new WaitForSeconds(1f);

            _controller.moveable = true;
            _controller.freezeFlip = false;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
