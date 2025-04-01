using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffFire : MonoBehaviour
{
    public int damage = 0;
    public GameObject effectEntity;

    EnemyController controller;

    private GameObject _effectObj;

    private void Awake() {
        if(TryGetComponent(out EnemyController ec)) controller = ec;
        else transform.root.GetComponent<EnemyController>();
    }

    private void Start() {
        _effectObj = Instantiate(effectEntity, transform.position, Quaternion.identity, transform);
        StartCoroutine(Tick());
    }
    
    IEnumerator Tick() {
        for (int i = 0; i < 4; i++)
        {
            controller.Hit(damage, transform.position);
            print($"fire debuff damage {damage} / "+i+" / "+gameObject.name);
            yield return new WaitForSeconds(1);
        }
        
        //Destroy(_effectObj);
        // Destroy(this);
        OnDisable();
    }

    private void OnDisable() {
        Destroy(_effectObj);
        Destroy(this);
    }
}
