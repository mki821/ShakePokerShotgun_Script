using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private SerializableDict<AudioClip> _audioSerializeDictionary;

    public int _masterVolume = 6;
    public int _bgmVolume = 6;
    public int _sfxVolume = 6;

    private Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;
    private AudioSource _uiAudioSource;
    private AudioSource _bgmAudioSource;
    public AudioMixerGroup _uiAudioMixer;
    public AudioMixerGroup _bgmAudioMixer;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        _audioSource = GetComponent<AudioSource>();
        _uiAudioSource = gameObject.AddComponent<AudioSource>();
        _bgmAudioSource = gameObject.AddComponent<AudioSource>();
        _uiAudioSource.outputAudioMixerGroup = _uiAudioMixer;
        _bgmAudioSource.outputAudioMixerGroup = _bgmAudioMixer;
        _bgmAudioSource.loop = true;

        _audioDictionary = _audioSerializeDictionary.GetDict();
    }

    public void PlaySound(string name) {
        if(_audioDictionary.ContainsKey(name)) {
            _audioSource.PlayOneShot(_audioDictionary[name]);
        }
        else Debug.LogError("[AudioMAnager] Not found clip name!");
    }

    public void UIPlaySound(string name) {
        if(_audioDictionary.ContainsKey(name)) {
            _uiAudioSource.PlayOneShot(_audioDictionary[name]);
        }
        else Debug.LogError("[AudioMAnager] Not found clip name!");
    }

    public void PlayBGM(string name)
    {
        if (_audioDictionary.ContainsKey(name))
        {
            _bgmAudioSource.Stop();
            _bgmAudioSource.clip = _audioDictionary[name];
            _bgmAudioSource.PlayOneShot(_audioDictionary[name]);
        }
        else Debug.LogError("[AudioMAnager] Not found clip name!");
    }
}

[System.Serializable]
public class SerializableDict<T>
{
    public List<SerializeData<T>> data;
    private Dictionary<string, T> dict = new Dictionary<string, T>();

    public Dictionary<string, T> GetDict() {
        for(int i = 0; i < data.Count; ++i) {
            dict.Add(data[i].key, data[i].value);
        }
        return dict;
    }
}

[System.Serializable]
public class SerializeData<T>
{
    public string key;
    public T value;
}
