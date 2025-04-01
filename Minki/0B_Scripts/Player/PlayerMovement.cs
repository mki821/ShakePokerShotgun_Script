using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    
    private Animator _animator;

    private void Awake() {
        _animator = (Animator)GetComponent("Animator");
    }
    
    private void Start() {
        _animator.runtimeAnimatorController = CharacterManager.instance.GetData().Item1.anim;
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(x, y);

        transform.position += direction.normalized * _speed * Time.deltaTime;

        _animator.SetBool("isMove", ((int)x | (int)y) != 0);
    }
}
