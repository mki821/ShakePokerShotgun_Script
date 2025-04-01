using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance = null;

    [SerializeField] private List<PoolSO> _poolList;

    private Dictionary<string, Queue<GameObject>> _poolQueue = new Dictionary<string, Queue<GameObject>>();

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        for(int i = 0; i < _poolList.Count; ++i) {
            _poolQueue[_poolList[i].name] = new Queue<GameObject>();
            for(int j = 0; j < _poolList[i].count; ++j) {
                GameObject obj = Instantiate(_poolList[i].prefab, transform.position, Quaternion.identity, transform);
                obj.SetActive(false);
                obj.name = obj.name.Replace("(Clone)", "");
                _poolQueue[_poolList[i].name].Enqueue(obj);
            }
        }
    }

    public GameObject Pop(string name, Vector3 position, float angle = 0f, Transform parent = null) {
        if(_poolQueue[name].Count <= 1) {
            GameObject oldObj = _poolQueue[name].Dequeue();
            GameObject newObj = Instantiate(oldObj, transform.position, Quaternion.identity, transform);
            newObj.name = newObj.name.Replace("(Clone)", "");
            _poolQueue[name].Enqueue(oldObj);
            _poolQueue[name].Enqueue(newObj);
        }

        GameObject obj = _poolQueue[name].Dequeue();
        obj.transform.parent = parent;
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
        obj.SetActive(true);

        return obj;
    }

    public void Push(string name, GameObject obj) {
        obj.transform.parent = transform;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.SetActive(false);
        _poolQueue[name].Enqueue(obj);
    }
}
