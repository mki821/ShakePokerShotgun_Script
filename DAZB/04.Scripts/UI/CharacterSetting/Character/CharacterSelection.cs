using UnityEngine;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public CharacterData[] characternDatas;
    public int nowSelectCharacterNumber;
    private TMP_Text text;
    private Animator anim;

    private void Awake() {
        text = GetComponentInChildren<TMP_Text>();
        anim = GetComponentInParent<Animator>();
    }

    private void Start() {
        nowSelectCharacterNumber = 0;
        SetCharacter(nowSelectCharacterNumber);
    }

    public void LeftButton(){
        nowSelectCharacterNumber--;
        if (nowSelectCharacterNumber <= 0) {
            nowSelectCharacterNumber = characternDatas.Length - 1;
        }
        SetCharacter(nowSelectCharacterNumber);
    }

    public void RightButton() {
        nowSelectCharacterNumber++;
        if (nowSelectCharacterNumber >= characternDatas.Length) {
            nowSelectCharacterNumber = 0;
        }
        SetCharacter(nowSelectCharacterNumber);
    }

    private void SetCharacter(int idx) {
        text.text = characternDatas[idx].name;
        anim.Play("Player_Idle_UI"+idx);
        CharacterManager.instance.SetChacracter(characternDatas[idx]);
    }
}
