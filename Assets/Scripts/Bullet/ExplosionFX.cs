using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class ExplosionFX : MonoBehaviour
{
    [SerializeField] string explosionSFX;

    void Start()
    {
        RuntimeManager.PlayOneShot(explosionSFX);
        Destroy(gameObject, 2f);
    }
}
