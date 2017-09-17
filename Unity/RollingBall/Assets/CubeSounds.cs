using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSounds : MonoBehaviour
{
    private AudioSource audioSource = null;

    private AudioClip impactClip = null;

    // Use this for initialization
    private void Start()
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = true;
        this.audioSource.spatialize = true;
        this.audioSource.spatialBlend = 1.0f;
        this.audioSource.dopplerLevel = 0.0f;
        this.audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        this.audioSource.maxDistance = 20f;

        this.impactClip = Resources.Load<AudioClip>("Cardboard_audio_1");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter()
    {
        this.audioSource.clip = this.impactClip;
        this.audioSource.Play();
    }
}