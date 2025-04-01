using UnityEngine;

public class GunView : MonoBehaviour
{
    private Camera _cam;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _cam = Camera.main;
        _spriteRenderer = (SpriteRenderer)transform.root.GetComponent("SpriteRenderer");
    }

    private void Update() {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - transform.position;

        if(transform.position.x == mousePos.x) return;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg);

        bool flip = transform.position.x < mousePos.x;
        transform.localScale = new Vector3(flip ? 1 : -1, 1, 1);
        _spriteRenderer.flipX = !flip;
    }
}
