using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider1 : MonoBehaviour {

    float Grap_F;
    public GameObject GunR;
    private void Update()
    {
        Grap_F =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(Grap_F);
    }
    private void OnCollisionStay(Collision c)
    {
        Debug.Log(c.transform.name);
        if (c.gameObject.tag == "Player" && Grap_F >= 0.8)
        {
            Debug.Log("ddd");
            GunR.SendMessage("H_changeR");
            Destroy(this.gameObject);
        }
    }
}
