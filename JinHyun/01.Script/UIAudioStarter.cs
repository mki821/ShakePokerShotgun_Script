using UnityEngine;

public class UIAudioStarter : MonoBehaviour
{
    public void ButtonClickSound()
    {
        AudioManager.Instance.UIPlaySound("ButtonClick");
    }
}
