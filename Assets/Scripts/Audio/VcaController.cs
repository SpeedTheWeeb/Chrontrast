using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class VcaController : MonoBehaviour
{
    public string vcaString;

    VCA vcaController;
    Slider slider;

    void Start()
    {
        vcaController = RuntimeManager.GetVCA(vcaString);
        slider = GetComponent<Slider>();
    }

    public void Volume(float volume)
    {
        vcaController.setVolume(volume);        
    }
}
