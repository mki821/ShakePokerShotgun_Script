using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] WeaponBullet _bullet;
    IWeaponEvent eventHandler;

    [SerializeField] GameObject default_weapon;
    
    private void Start() {
        SetWeapon(CharacterManager.instance.GetData().Item2?.weaponPrf ==  null ? default_weapon : CharacterManager.instance.GetData().Item2?.weaponPrf);
    }

    public void SetWeapon(GameObject weaponEntity) {
        var weapon = Instantiate(weaponEntity, transform);
        eventHandler = weapon.GetComponent<IWeaponEvent>();
        
        eventHandler.Init(_bullet);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            eventHandler?.MouseDown();
        } else if (Input.GetMouseButtonUp(0)) {
            eventHandler?.MouseUp();
        }
    }
}

interface IWeaponEvent {
    void Init(WeaponBullet _weaponBullet);
    void MouseDown();
    void MouseUp();
}