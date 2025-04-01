using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffFreeze : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    EnemyController _controller;
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _controller = GetComponent<EnemyController>();
    }

    private void Start() {
        print("freeze Start");
        StartCoroutine(Freeze());
    }

    IEnumerator Freeze()
    {
        _controller.moveable = false;
        _spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(2);
        OnDisable();
    }

    private void OnDisable()
    {
        print("∫Ò»∞");
        _spriteRenderer.color = Color.white;
        _controller.moveable = true;
        Destroy(this);
    }
}
