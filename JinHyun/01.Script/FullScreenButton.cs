using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenButton : MonoBehaviour
{
    public Image fullScreenImg;

    private void Start()
    {
        fullScreenImg.enabled = Screen.fullScreen;
    }

    public void ClickButton()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreenImg.enabled = !fullScreenImg.enabled;
    }
}
