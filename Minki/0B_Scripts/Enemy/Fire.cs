using UnityEngine;

public class Fire : MonoBehaviour
{
    [HideInInspector] public Vector3 center = Vector2.zero;

    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _speed = 3f;

    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;

        Vector3 direction = center - transform.position;
        transform.position += direction.normalized * _speed * Time.deltaTime;

        if(_timer >= _lifeTime) {
            _timer = 0;
            PoolManager.Instance.Push("Fire", gameObject);
        }
    }
}
