using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider : MonoBehaviour
{
    float Grap_F;
    private void Update()
    {
        Grap_F =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(Grap_F);
    }
    private void OnCollisionStay(Collision c)
    {
        Debug.Log(c.transform.name);
        if (c.gameObject.tag == "Player"&&Grap_F>=0.8)
        {
            Debug.Log("ddd");
            c.gameObject.SendMessage("H_ChangeL");
            Destroy(this.gameObject);
        }
    }
}

