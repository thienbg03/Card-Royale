using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource AudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkSound()
    {
        if (PlayerManager.Instance.GameManager.CurrentPhase != GamePhase.CombatPhase) 
            return;

        AudioSource.clip = clips[0];
        AudioSource.Play();
    }

    public void PlaySwordSound(int index)
    {
        if (PlayerManager.Instance.GameManager.CurrentPhase != GamePhase.CombatPhase)
            return;
        AudioSource.clip = clips[index];
        AudioSource.Play();
    }

    public void PlayDodgeSound()
    {
        if (PlayerManager.Instance.GameManager.CurrentPhase != GamePhase.CombatPhase)
            return;
        AudioSource.clip = clips[3];
        AudioSource.Play();
    }
}
