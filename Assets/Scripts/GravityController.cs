﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityController : MonoBehaviour
{
    private static GravityController instance;

    public static GravityController GetInstance()
    {
        if (instance)
        {
            return instance;
        }

        instance = FindObjectOfType<GravityController>();
        return instance;
    }
    
    public enum Direction
    {
        Forward, Back, Left, Right, Up, Down
    }

    public Direction direction = Direction.Down;
    public float acceleration = 9.8f;
    
    private HashSet<GravityListener> gravityListeners = new HashSet<GravityListener>();
    private Direction _lastDirection = Direction.Down;
    private AudioSource _audioSource;

    private void Start()
    {
        _lastDirection = direction;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetGravityDirectionDown();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SetGravityDirectionUp();
        }
        
    }

    private void FixedUpdate()
    {
        if (!direction.Equals(_lastDirection))
        {
            Vector3 g = Vector3.down * acceleration;
            switch (direction)
            {
                case Direction.Forward:
                    g = Vector3.forward * acceleration;
                    break;
                case Direction.Back:
                    g = Vector3.back * acceleration;
                    break;
                case Direction.Left:
                    g = Vector3.left * acceleration;
                    break;
                case Direction.Right:
                    g = Vector3.right * acceleration;
                    break;
                case Direction.Up:
                    g = Vector3.up * acceleration;
                    break;
                case Direction.Down:
                    g = Vector3.down * acceleration;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _lastDirection = direction;
            Physics.gravity = g;
            foreach (GravityListener listener in gravityListeners)
            {
                listener.OnGravityChange(g);
            }
            
            _audioSource.Play();
        }
    }

    public void register(GravityListener gravityListener)
    {
        gravityListeners.Add(gravityListener);
    }

    public void unregister(GravityListener gravityListener)
    {
        gravityListeners.Remove(gravityListener);
    }

    public void SetGravityDirectionUp()
    {
        this.direction = Direction.Up;
    }
    
    public void SetGravityDirectionDown()
    {
        this.direction = Direction.Down;
    }
    
    public void SetGravityDirectionLeft()
    {
        this.direction = Direction.Left;
    }
    
    public void SetGravityDirectionRight()
    {
        this.direction = Direction.Right;
    }
    
    public void SetGravityDirectionForward()
    {
        this.direction = Direction.Forward;
    }
    
    public void SetGravityDirectionBack()
    {
        this.direction = Direction.Back;
    }
}
