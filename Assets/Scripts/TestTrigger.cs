﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.Player || other.gameObject.layer == Layers.Usable )
        {
//            AutoUsable usable = GetComponent<AutoUsable>();
//            if (usable)
//            {
//                usable.Use();
//            }
            if (CompareTag("puzzleArea3"))
            {
                OpenDoor2.pressurePlateCounter++;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Player || other.gameObject.layer == Layers.Usable)
        {
//            Usable usable = GetComponent<Usable>();
//            if (usable)
//            {
//                usable.Use();
//            }
            if (CompareTag("puzzleArea3"))
            {
                OpenDoor2.pressurePlateCounter--;
            }
        }
    }
    

}
