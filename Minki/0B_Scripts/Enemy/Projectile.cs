using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _delayTime = 0.5f;

    private float _timer = 0f;
    private bool _first = true;
    private bool _create = true;

    private void OnEnable() {
        if(_create) {
            _create = false;
            return;
        }
        
        AudioManager.Instance.PlaySound("Unequip");
    }

    private void Update() {
        _timer += Time.deltaTime;
        if(_timer < _delayTime) return;

        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        Shot();

        if(_timer >= _lifeTime + _delayTime) {
            _timer = 0f;
            _first = true;
            PoolManager.Instance.Push("BossProjectile", gameObject);
        }
    }

    private void Shot() {
        if(_first) {
            _first = false;
            AudioManager.Instance.PlaySound("Wind");
        }
    }
}
