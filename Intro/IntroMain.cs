using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroMain : MonoBehaviour
{
    [SerializeField] VolumeProfile _volume;
    private void Awake() {
        if (_volume.TryGet<Bloom>(out var _bloom))
            _bloom.intensity.Override(0);
    }
}
