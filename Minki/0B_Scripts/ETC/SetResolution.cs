using UnityEngine;
using TMPro;

public class SetResolution : MonoBehaviour
{
    public FullScreenMode fullScreen;

    private TMP_Dropdown _dropdown;

    private void Awake() {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start() {
        if(Screen.width >= 3840 && Screen.height >= 2160) {
            Screen.SetResolution(3840, 2160, fullScreen);
            _dropdown.value = 0;
        }
        else if(Screen.width >= 2560 && Screen.height >= 1440) {
            Screen.SetResolution(2560, 1440, fullScreen);
            _dropdown.value = 1;
        }
        else if(Screen.width >= 1920 && Screen.height >= 1080) {
            Screen.SetResolution(1920, 1080, fullScreen);
            _dropdown.value = 2;
        }
        else if(Screen.width >= 1280 && Screen.height >= 720) {
            Screen.SetResolution(1280, 720, fullScreen);
            _dropdown.value = 3;
        }
        else {
            Screen.SetResolution(720, 480, fullScreen);
            _dropdown.value = 4;
        }

        _dropdown.onValueChanged.AddListener(Resloution);
    }

    private void OnEnable()
    {
        _dropdown.value = GetDropDownValue();
    }

    private int GetDropDownValue()
    {
        if (Screen.currentResolution.width >= 3840 && Screen.currentResolution.height >= 2160) return 0;
        else if (Screen.currentResolution.width >= 2560 && Screen.currentResolution.height >= 1440) return 1;
        else if (Screen.currentResolution.width >= 1920 && Screen.currentResolution.height >= 1080) return 2;
        else if (Screen.currentResolution.width >= 1280 && Screen.currentResolution.height >= 720) return 3;
        else return 4;
    }

    private void Resloution(int value) {
        switch(value) {
            case 0: Screen.SetResolution(3840, 2160, fullScreen); break;
            case 1: Screen.SetResolution(2560, 1440, fullScreen); break;
            case 2: Screen.SetResolution(1920, 1080, fullScreen); break;
            case 3: Screen.SetResolution(1280, 720, fullScreen); break;
            case 4: Screen.SetResolution(720, 480, fullScreen); break;
        }
    }
}
