using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public CharacterData currentCharacter;
    public WeaponData currentWeapon;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public (CharacterData, WeaponData) GetData() {
        return (currentCharacter, currentWeapon);
    }

    public void SetChacracter(CharacterData characterData) {
        currentCharacter = characterData;
    }

    public void SetWeapon(WeaponData weaponData) {
        currentWeapon = weaponData;
    }
}
