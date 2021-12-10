using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TutorialBGM : MonoBehaviour
{
    public EventInstance bgmTut;
    PLAYBACK_STATE playbackState;

    void Start()
    {        
        bgmTut = RuntimeManager.CreateInstance("event:/bgm/tutorial");
        bgmTut.start();
        bgmTut.getPlaybackState(out playbackState);
    }
}
