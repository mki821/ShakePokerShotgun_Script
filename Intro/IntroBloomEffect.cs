using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class IntroBloomEffect : MonoBehaviour
{
   [SerializeField] VolumeProfile _volume;
   [SerializeField] Image _screen;
   Bloom _bloom;

   private void Awake() {
    _volume.TryGet<Bloom>(out _bloom);
   }

   private void Start() {
      print("Hello world!");
      StartCoroutine(StartHandler());
      StartCoroutine(WhiteScreen());
   }

   IEnumerator StartHandler() {
      float time = 0;
      while (time < 1) {
         yield return null;
         
         time  += Time.deltaTime / 20;
         _bloom.intensity.Override(Mathf.Lerp(_bloom.intensity.GetValue<float>(), 500, time));
      }
   }

   IEnumerator WhiteScreen() {
      float time = 0;
      while (time < 1) {
         yield return null;
         
         time  += Time.deltaTime / 1;
         _screen.color = new(1,1,1, time);
      }
   }
}
