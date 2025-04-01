using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 0.4f;

    private float _timer = 0f;
    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;
    }

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= _spawnDelay) {
            Vector2 minPos = _cam.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 maxPos = _cam.ViewportToWorldPoint(new Vector2(1, 1));
            int rndPos = Random.Range(0, 4);
            switch(rndPos) {
                case 0:
                    PoolManager.Instance.Pop("Enemy", new Vector2(minPos.x, Random.Range(minPos.y, maxPos.y)), parent: transform);
                    break;
                case 1:
                    PoolManager.Instance.Pop("Enemy", new Vector2(maxPos.x, Random.Range(minPos.y, maxPos.y)), parent: transform);
                    break;
                case 2:
                    PoolManager.Instance.Pop("Enemy", new Vector2(Random.Range(minPos.x, maxPos.x), minPos.y), parent: transform);
                    break;
                case 3:
                    PoolManager.Instance.Pop("Enemy", new Vector2(Random.Range(minPos.x, maxPos.x), maxPos.y), parent: transform);
                    break;
            }
            _timer = 0f;
        }
    }
}
