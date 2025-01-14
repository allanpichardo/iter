﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float followDistance = 10.0f;
    public float followHeight = 4.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 5.0f;
    [Range(-45.0f, -15.0f)]
    public float lookMinAngle = -20.0f;
    [Range(15.0f, 45.0f)]
    public float lookMaxAngle = 20.0f;
    
    private Vector2 _rotation = Vector2.zero;
    private float _wantedRotationAngle;
    private float _wantedHeight;
    private float _currentRotationAngle;
    private float _currentHeight;
    private Quaternion _currentRotation;

    void Update ()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _rotation.x += IsGravityNegative() ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");
        _rotation.x = Mathf.Clamp(_rotation.x, lookMinAngle, lookMaxAngle);
        
        // Calculate the current rotation angles
        _wantedRotationAngle = player.eulerAngles.y;
        _wantedHeight = player.position.y + followHeight;
        _currentRotationAngle = transform.eulerAngles.y;
        _currentHeight = transform.position.y;
        // Damp the rotation around the y-axis
        _currentRotationAngle = Mathf.LerpAngle (_currentRotationAngle, _wantedRotationAngle, rotationDamping * Time.deltaTime);
        // Damp the height
        _currentHeight = Mathf.Lerp (_currentHeight, _wantedHeight, heightDamping * Time.deltaTime);
        // Convert the angle into a rotation
        _currentRotation = Quaternion.Euler (_rotation.x, _currentRotationAngle, 0);
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = player.position;
        transform.position -= _currentRotation * Vector3.forward * followDistance;
        // Set the height of the camera
        transform.position = new Vector3 (transform.position.x, _currentHeight, transform.position.z);
        
        Vector3 position = player.position - (player.forward * followDistance) + (player.up * followHeight);
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        Vector3 angles = rotation.eulerAngles;
        angles.x = _rotation.x * speed;
        angles.z = player.rotation.eulerAngles.z;
        transform.SetPositionAndRotation(position, Quaternion.Euler(angles));
    }

    private bool IsGravityNegative()
    {
        Vector3 gravNormed = Physics.gravity.normalized;
        return (gravNormed.x + gravNormed.y + gravNormed.z) < 0.0f;
    }
}
