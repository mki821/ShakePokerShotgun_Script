using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSetter : MonoBehaviour
{
    public SpriteRenderer sr;
    private void OnEnable() {
        sr.sprite = CharacterManager.instance.GetData().Item1.sprite;
    }
}
