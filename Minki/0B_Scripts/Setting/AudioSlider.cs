using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Sprite[] _cards;
    [SerializeField] private string _mixerName;

    private Slider _slider;
    private Image _image;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _image = transform.GetChild(2).GetChild(0).GetComponent<Image>();

        _slider.onValueChanged.AddListener(SliderChange);
    }

    private void Start() {
        switch(_mixerName) {
            case "Master": _slider.value = AudioManager.Instance._masterVolume; break;
            case "BGM": _slider.value = AudioManager.Instance._bgmVolume; break;
            case "SFX": _slider.value = AudioManager.Instance._sfxVolume; break;
        }
    }

    private void SliderChange(float value) {
        int intValue = (int)value;

        _image.sprite = _cards[intValue];
        text.text = $"Volume : {intValue}";
        float soundValue = (intValue - 1) / 12f;
        if (intValue == 0)
        {
            print("焼艦");
            print(soundValue);
            print("---------------");
            _mixer.SetFloat(_mixerName, -80f);
        }
        else
        {
            print("ししけいし");
            print(soundValue);
            print(Mathf.Lerp(-40f, 0f, soundValue));
            print("---------------");
            _mixer.SetFloat(_mixerName, Mathf.Lerp(-40f, 0f, soundValue));
        }

            switch (_mixerName) {
            case "Master": AudioManager.Instance._masterVolume = intValue; break;
            case "BGM": AudioManager.Instance._bgmVolume = intValue; break;
            case "SFX": AudioManager.Instance._sfxVolume = intValue; break;
        }
    }
}
