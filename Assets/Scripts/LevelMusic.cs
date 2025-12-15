using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelMusic : MonoBehaviour
{
    public AudioClip levelMusic;

    void Start()
    {
        AudioManager.Instance.PlayMusic(levelMusic);
    }
}