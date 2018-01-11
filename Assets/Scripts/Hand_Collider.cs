﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider : MonoBehaviour
{

    private void Update()
    {
        float Grap_F =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(Grap_F);
    }
    private void OnTriggerEnter(Collider c)
    {
        
        if (c.gameObject.tag == "PH")
        {
            Debug.Log("ddd");
            c.gameObject.SendMessage("H_ChangeL");
        }
    }
}

