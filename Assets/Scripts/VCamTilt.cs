﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class VCamTilt : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineComposer _composer;
    private float _mouseY = 0.0f;
    private float _yOffset;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        _yOffset = _composer.m_TrackedObjectOffset.y;
    }

    private void Update()
    {
        TrackVertical();
    }

    private void TrackVertical()
    {
        _mouseY += Input.GetAxis("Mouse Y");
        _composer.m_TrackedObjectOffset.y = _yOffset + _mouseY;
    }
    
    private bool IsGravityNegative()
    {
        Vector3 gravNormed = Physics.gravity.normalized;
        return (gravNormed.x + gravNormed.y + gravNormed.z) < 0.0f;
    }
}
