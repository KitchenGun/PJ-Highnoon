using System.Collections;
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
    private void OnCollisionEnter(Collision c)
    {
        
        if (c.gameObject.tag=="PH")
        {
            c.gameObject.SendMessage("H_ChangeL");
        }
    }
}
