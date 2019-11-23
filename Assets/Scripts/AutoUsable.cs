﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AutoUsable : MonoBehaviour
{
    public UnityEvent onStepOn;
    public UnityEvent onStepOff;
    public AudioClip audioClip;

    private BoxCollider _boxCollider;
    private AudioSource _audioSource;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player || other.gameObject.layer == Layers.Companion)
        {
            onStepOn?.Invoke();
            _audioSource.PlayOneShot(audioClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Player || other.gameObject.layer == Layers.Companion)
        {
            onStepOff?.Invoke();
        }
    }
}