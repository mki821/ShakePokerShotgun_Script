using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private float _timer = 0f;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        _animator.Play(0);
    }

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= 0.2f) {
            _timer = 0f;
            PoolManager.Instance.Push("PlayerHit", gameObject);
        }
    }
}
