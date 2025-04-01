using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum WeaponType {
    PUMP,
    DOUBLEBARREL,
    BULLPUP 

}

[System.Serializable]
public class WeaponData {
    public WeaponType WeaponType;
    public GameObject weaponPrf;
}
