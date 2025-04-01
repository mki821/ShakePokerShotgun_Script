using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] string _startBGM;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(_startBGM);
    }
}
