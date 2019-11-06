﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool isDoorLocked = true;
    public float openingSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        isDoorLocked = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDoorLocked &&  transform.position.y <= 134)
        {
            transform.position += new Vector3(0, openingSpeed  * Time.deltaTime, 0);

        }
    }

    public void UnlockDoor()
    {
        isDoorLocked = false; 
    }
}