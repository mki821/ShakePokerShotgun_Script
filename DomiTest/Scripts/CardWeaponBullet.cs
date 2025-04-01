using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWeaponBullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5;
    public Vector2 customDir;
    public event Func<Collider2D, bool> OnCallback;
    public Transform standEntity;
    
    private void Start() 
    {
        AudioManager.Instance.PlaySound("GaugeSingle");

        if (standEntity == null) {
            Debug.LogWarning("StandEntity not define");
        }
    }

    void Update()
    {
        transform.position += (customDir == Vector2.zero ? transform.right : (Vector3)customDir) * Time.deltaTime * speed;
        if (standEntity && Vector2.Distance(standEntity.position, transform.position) > 10 * 10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("---------- bullet collider logger");
        print(other.gameObject.name);
        print(other.TryGetComponent<EnemyController>(out var _));
        if (!enabled) return;
        EnemyController controller = null;
        if(!other.TryGetComponent<EnemyController>(out controller))
            if(!other.transform.root.TryGetComponent<EnemyController>(out controller)) return;

        // Cancel Event
        if (OnCallback != null && !OnCallback.Invoke(other)) return;

        print("---------- bullet collider logger 2 (hit)");
        print($"{gameObject.name} -> {other.gameObject.name} ({damage})");
        controller.Hit(damage, transform.position);
        // Destroy(gameObject);
    }
}
