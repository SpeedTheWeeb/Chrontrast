using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class MenuBGM : MonoBehaviour
{
    public EventInstance bgmLoop;
    PLAYBACK_STATE playbackState;

    private void Start()
    {
        bgmLoop = RuntimeManager.CreateInstance("event:/bgm/menu");
        bgmLoop.start();
        bgmLoop.getPlaybackState(out playbackState);
    }

    public void StartGameSFX()
    {
        RuntimeManager.PlayOneShot("event:/sfx/menu/start");
    }

    public void ClickSFX()
    {
        RuntimeManager.PlayOneShot("event:/sfx/menu/click");
    }
}
