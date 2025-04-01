using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private float _boomDelay;
    [SerializeField] private bool _c = false;

    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= _boomDelay) {
            AudioManager.Instance.PlaySound("Dive");
            PoolManager.Instance.Push(_c ? "BoomC" : "Boom", gameObject);
            _timer = 0f;
        }
    }

    public void Shake() {
        CamManager.StartShake(9f, 0.2f);
    }
}
