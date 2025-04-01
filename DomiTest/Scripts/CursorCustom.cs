using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCustom : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] bool PlayOnAwake = false;

    private void Awake() {
        if (PlayOnAwake) {
            Cursor.SetCursor(cursor, new (cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
        } else {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
